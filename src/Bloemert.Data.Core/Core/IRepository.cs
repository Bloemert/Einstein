using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bloemert.Data.Core
{

	public interface IRepository<E>
		where E : IEntity
	{
		string TableName { get; set; }
		string EntityTypeName { get; set; }
		Regex ExcludePropertyMatch { get; set; }


		IEnumerable<DbColumnInfo> ReadMetadataFromTable();
		
		IList<string> GetColumnsFromMetaData(RequestedColumns cols = RequestedColumns.ALL, IList<string> excludedColumns = null);


		E NewEntity();


		E GetEntity(int id);
		Task<E> GetEntityAsync(int id);

		T GetEntity<T>(string qry, object param);
		Task<T> GetEntityAsync<T>(string qry, object param);


		E SaveEntity(E entity);
		Task<E> SaveEntityAsync(E entity);


		bool DeleteEntity(int id);
		Task<bool> DeleteEntityAsync(int id);

		bool DeleteEntity(E entity);
		Task<bool> DeleteEntityAsync(E entity);


		IList<E> ListEntity();
		Task<IList<E>> ListEntityAsync();

		IList<E> ListEntity(string query);
		Task<IList<E>> ListEntityAsync(string query);

		IList<E> ListEntity(string query, object param);
		Task<IList<E>> ListEntityAsync(string query, object param);

		IList<T> ListEntity<T>(string query);
		Task<IList<T>> ListEntityAsync<T>(string query);

		IList<T> ListEntity<T>(string query, object param);
		Task<IList<T>> ListEntityAsync<T>(string query, object param);

	}
}
