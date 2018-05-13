using System.Collections.Generic;
using System.Linq;
using Dapper;
using System;
using Serilog;
using System.Data;
using static Dapper.SqlMapper;
using System.Threading.Tasks;

namespace Bloemert.Data.Core
{
	public class DefaultDbExecutor : IDbExecutor
	{
		// Dependency Injection into Property by IoC
		private ILogger Log { get; }

		public IDbConnectionFactory ConnectionFactory { get; set; }


		public DefaultDbExecutor(ILogger log, IDbConnectionFactory connFactory)
		{
			Log = log;
			ConnectionFactory = connFactory;
		}



		public E Select<E>(string query, IDbParameters dbParams)
		{
			E result;
			using (var conn = ConnectionFactory.Create())
			{
				result = conn.Query<E>(query, dbParams != null ? dbParams.DynamicParameters : null).FirstOrDefault();
			}

			return result;
		}

		public async Task<E> SelectAsync<E>(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				var result = await conn.QueryAsync<E>(query, dbParams != null ? dbParams.DynamicParameters : null);
				return result.FirstOrDefault();
			}
		}




		public dynamic Select(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				return conn.Query(query, dbParams != null ? dbParams.DynamicParameters : null).FirstOrDefault();
			}
		}

		public async Task<dynamic> SelectAsync(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				var result = await conn.QueryAsync(query, dbParams != null ? dbParams.DynamicParameters : null);
				return result.FirstOrDefault();
			}
		}



		public IList<E> List<E>(string query, IDbParameters dbParams)
		{
			List<E> result;
			using (var conn = ConnectionFactory.Create())
			{
				result = conn.Query<E>(query, dbParams != null ? dbParams.DynamicParameters : null).ToList();
			}

			return result;
		}

		public async Task<IList<E>> ListAsync<E>(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				var result = await conn.QueryAsync<E>(query, dbParams != null ? dbParams.DynamicParameters : null);
				return result.ToList();
			}
		}



		public IEnumerable<dynamic> List(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				return conn.Query<dynamic>(query, dbParams != null ? dbParams.DynamicParameters : null);
			}
		}
		
		public async Task<IEnumerable<dynamic>> ListAsync(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				return await conn.QueryAsync<dynamic>(query, dbParams != null ? dbParams.DynamicParameters : null);
			}
		}



		public IDictionary<KT, VT> Dictionary<KT, VT>(string query, IDbParameters dbParams)
		{
			IDictionary<KT, VT> result;
			using (var conn = ConnectionFactory.Create())
			{
				result = conn.Query(query, dbParams != null ? dbParams.DynamicParameters : null).ToDictionary(row => (KT)row.Key, row => (VT)row.Value);
			}

			return result;
		}

		public async Task<IDictionary<KT, VT>> DictionaryAsync<KT, VT>(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				var result = await conn.QueryAsync(query, dbParams != null ? dbParams.DynamicParameters : null);
				return result.ToDictionary(row => (KT)row.Key, row => (VT)row.Value);
			}
		}



		public FT ExecuteScalar<FT>(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				return conn.ExecuteScalar<FT>(query, dbParams != null ? dbParams.DynamicParameters : null);
			}
		}

		public async Task<FT> ExecuteScalarAsync<FT>(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				return await conn.ExecuteScalarAsync<FT>(query, dbParams != null ? dbParams.DynamicParameters : null);
			}
		}



		public void Execute(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				conn.Execute(query, dbParams != null ? dbParams.DynamicParameters : null);
			}
		}

		public async Task ExecuteAsync(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				await conn.ExecuteAsync(query, dbParams != null ? dbParams.DynamicParameters : null);
			}
		}



		public E ExecuteAndQuery<E>(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				return conn.Query<E>(query, dbParams != null ? dbParams.DynamicParameters : null).FirstOrDefault();
			}
		}

		public async Task<E> ExecuteAndQueryAsync<E>(string query, IDbParameters dbParams)
		{
			using (var conn = ConnectionFactory.Create())
			{
				var qryResult = await conn.QueryAsync<E>(query, dbParams != null ? dbParams.DynamicParameters : null);
				return qryResult.FirstOrDefault();
			}
		}

	}
}
