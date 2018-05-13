using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloemert.Data.Core
{
	public interface IDbExecutor
	{
		IDbConnectionFactory ConnectionFactory { get; set; }

		E Select<E>(string query, IDbParameters param = null);
		Task<E> SelectAsync<E>(string query, IDbParameters param = null);

		dynamic Select(string query, IDbParameters param = null);
		Task<dynamic> SelectAsync(string query, IDbParameters param = null);



		IList<E> List<E>(string query, IDbParameters param = null);
		Task<IList<E>> ListAsync<E>(string query, IDbParameters param = null);

		IEnumerable<dynamic> List(string query, IDbParameters param = null);
		Task<IEnumerable<dynamic>> ListAsync(string query, IDbParameters param = null);

		IDictionary<KT, VT> Dictionary<KT, VT>(string query, IDbParameters param = null);
		Task<IDictionary<KT, VT>> DictionaryAsync<KT, VT>(string query, IDbParameters param = null);


		FT ExecuteScalar<FT>(string query, IDbParameters param = null);
		Task<FT> ExecuteScalarAsync<FT>(string query, IDbParameters param = null);

		void Execute(string query, IDbParameters param = null);
		Task ExecuteAsync(string query, IDbParameters param = null);

		E ExecuteAndQuery<E>(string query, IDbParameters param = null);
		Task<E> ExecuteAndQueryAsync<E>(string query, IDbParameters param = null);

	}
}
 