using Autofac;
using Bloemert.Data.Core.Templates;
using Bloemert.Lib.Common;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bloemert.Data.Core
{

	public interface ICommonRepositoryDependencies
	{
		ILifetimeScope IoC { get; }

		IDbConnectionFactory ConnectionFactory { get; }

		ILogger Log { get; }

		IDbExecutor Db { get; }

	}



	public class BaseRepository<R,E> : IRepository<E>
		where R : IRepository<E>
		where E : IEntity
	{
		public virtual string TableName { get; set; }

		public virtual string EntityTypeName { get; set; } = typeof(E).Name;

		public virtual Regex ExcludePropertyMatch { get; set; } = new Regex(@"^\s*$");


		public ICommonRepositoryDependencies Crd { get; }

		protected IQueryTemplate QueryTemplate { get; set; }


		protected ILifetimeScope IoC { get { return Crd.IoC; } }
		protected IDbExecutor Db { get { return Crd.Db; } }
		protected ILogger Log { get { return Crd.Log; } }


		public BaseRepository(ICommonRepositoryDependencies crd)
		{
			if ( String.IsNullOrEmpty(TableName))
			{
				crd.Log.Error("No Table name provided!");
			}

			Crd = crd;

			if (IoC.IsRegisteredWithName<IQueryTemplate>(TableName))
			{
				QueryTemplate = IoC.ResolveNamed<IQueryTemplate>(TableName, new NamedParameter("repository", this));
			}
			else
			{
				QueryTemplate = new BaseQueryTemplate<E>(this);
			}

			QueryTemplate.EntityMetadata = ReadMetadataFromTable();
		}


		protected System.Type BaseEntityType
		{
			get { return typeof(E); }
		}

		protected readonly string _parameterPrefix = "@";





		public IEnumerable<DbColumnInfo> ReadMetadataFromTable()
		{
			IDbConnection dbConnection = Db.ConnectionFactory.Create();

			return dbConnection.Query<DbColumnInfo>(QueryTemplate.CreateMetaDataQuery());
		}

		public virtual IList<string> GetColumnsFromMetaData(RequestedColumns cols = RequestedColumns.ALL, IList<string> excludedColumns = null)
		{
			return (from ci in QueryTemplate.EntityMetadata
							where (cols == RequestedColumns.ALL || 
											(!ci.IsExcludedProperty || !cols.HasFlag(RequestedColumns.NO_EXCLUDED) ) &&
											(!ci.IsIdentity || !cols.HasFlag(RequestedColumns.NO_PRIMARYKEY) ) &&
											(!ci.IsComputed || !cols.HasFlag(RequestedColumns.NO_COMPUTED)) &&
											(excludedColumns == null || !excludedColumns.Contains(ci.ColumnName)))
							select ci.ColumnName).ToList();
		}


		public virtual E NewEntity()
		{
			return (E)Activator.CreateInstance(BaseEntityType);
		}



		public virtual E GetEntity(int id)
		{
			var result = default(E);

			if (id <= 0)
				return result;

			return Db.Select<E>(QueryTemplate.CreateSelectQuery(), DbParameters.Create(new { id = id }));
		}

		public async virtual Task<E> GetEntityAsync(int id)
		{
			var result = default(E);

			if (id > 0)
			{
				result = await Db.SelectAsync<E>(QueryTemplate.CreateSelectQuery(), DbParameters.Create(new { id = id }));
			}

			return result;
		}


		public virtual T GetEntity<T>(string qry, object param)
		{
			return Db.Select<T>(qry, DbParameters.Create(param));
		}

		public virtual async Task<T> GetEntityAsync<T>(string qry, object param)
		{
			return await Db.SelectAsync<T>(qry, DbParameters.Create(param));
		}



		public virtual E SaveEntity(E entity)
		{
			IDbParameters dbParameters = DbParameters.Create(entity);

			if (entity.Id > 0)
			{ 
				this.Db.Execute(QueryTemplate.CreateUpdateQuery(), dbParameters);
			}
			else
			{
				entity = Db.ExecuteAndQuery<E>(QueryTemplate.CreateInsertQuery(), dbParameters);
			}

			return entity;
		}


		public virtual async Task<E> SaveEntityAsync(E entity)
		{
			IDbParameters dbParameters = DbParameters.Create(entity);

			if (entity.Id > 0)
			{
				await this.Db.ExecuteAsync(QueryTemplate.CreateUpdateQuery(), dbParameters);
			}
			else
			{
				entity = await Db.ExecuteAndQueryAsync<E>(QueryTemplate.CreateInsertQuery(), dbParameters);
			}

			return entity;
		}



		public virtual bool DeleteEntity(int id)
		{
			var entity = GetEntity(id);
			if (entity != null)
			{
				return DeleteEntity(entity);
			}

			return false;
		}
		public virtual async Task<bool> DeleteEntityAsync(int id)
		{
			var entity = await GetEntityAsync(id);
			if (entity != null)
			{
				return await DeleteEntityAsync(entity);
			}

			return false;
		}

		
		public virtual bool DeleteEntity(E entity)
		{
			if (entity != null)
			{
				entity.Deleted = true;
				SaveEntity(entity);

				return true;
			}
			return false;
		}

		public virtual async Task<bool> DeleteEntityAsync(E entity)
		{
			if (entity != null)
			{
				entity.Deleted = true;
				await SaveEntityAsync(entity);

				return true;
			}
			return false;
		}




		public virtual IList<E> ListEntity()
		{
			return Db.List<E>(QueryTemplate.CreateListQuery(), null).ToList();
		}
		public virtual async Task<IList<E>> ListEntityAsync()
		{
			var result = await Db.ListAsync<E>(QueryTemplate.CreateListQuery(), null);
			return result.ToList();
		}


		public virtual IList<E> ListEntity(string query)
		{
			return ListEntity<E>(query, null);
		}

		public virtual async Task<IList<E>> ListEntityAsync(string query)
		{
			return await ListEntityAsync<E>(query, null);
		}


		public virtual IList<E> ListEntity(string query, object param)
		{
			return ListEntity<E>(query, param);
		}
		public virtual async Task<IList<E>> ListEntityAsync(string query, object param)
		{
			return await ListEntityAsync<E>(query, param);
		}


		public virtual IList<T> ListEntity<T>(string query)
		{
			return Db.List<T>(query, null);
		}
		public virtual async Task<IList<T>> ListEntityAsync<T>(string query)
		{
			return await Db.ListAsync<T>(query, null);
		}

		public virtual IList<T> ListEntity<T>(string query, object param)
		{
			return Db.List<T>(query, DbParameters.Create(param));
		}
		public virtual async Task<IList<T>> ListEntityAsync<T>(string query, object param)
		{
			return await Db.ListAsync<T>(query, DbParameters.Create(param));
		}


		void PagedListEntity()//ref IPagedList<E> in_out)
		{
			using (IDbConnection conn = Db.ConnectionFactory.Create())
			{
				SqlMapper.GridReader grid = conn.QueryMultiple(QueryTemplate.CreatePagedListQuery("", new string[]{ "ID" } ));
				grid.ReadSingle<int>();
				grid.ReadSingle<int>();
				grid.Read<E>();
			}

		}


		/*
		protected virtual string GetSelectQuery()
		{
			return GetSelectQuery(int.MinValue);
		}
		protected virtual string GetSelectQuery(int entityId)
		{
			StringBuilder result = new StringBuilder("SELECT ");
			var i = 0;

			foreach (var pi in BaseEntityType.GetProperties()) 
			{
				if (i > 0)
				{
					result.Append(", ");
				}

				result.Append(pi.Name);

				i++;
			}

			result.Append($" FROM {TableName}");
			result.Append($" WHERE Deleted = 0 ");

			if (entityId > 0)
			{
				result.Append($" AND ID = {_parameterPrefix}id");
			}

			return result.ToString();
		}

		protected virtual string GetUpdateQuery()
		{
			var i = 0;
			StringBuilder result = new StringBuilder($"UPDATE {TableName} SET ");
			foreach (var pi in BaseEntityType.GetProperties().Where(x => !ExcludePropertyMatch.IsMatch(x.Name)))
			{
				if (!pi.Name.Equals("Id"))
				{
					if (i > 0)
					{
						result.Append(", ");
					}

					result.Append($"{pi.Name} = {_parameterPrefix}{pi.Name}");
					i++;
				}
			}

			result.Append($" WHERE ID = {_parameterPrefix}Id");

			return result.ToString();
		}

		protected virtual string GetUpdateAndQuery(E values)
		{
			return GetUpdateQuery();
		}


		protected virtual string GetInsertQuery()
		{
			var i = 0;
			StringBuilder result = new StringBuilder($"INSERT INTO {TableName} (");
			StringBuilder valueQry = new StringBuilder();

			foreach (var pi in BaseEntityType.GetProperties().Where(x => !ExcludePropertyMatch.IsMatch(x.Name)))
			{
				if (!pi.Name.Equals("Id"))
				{
					if (i > 0)
					{
						result.Append(", ");
						valueQry.Append(", ");
					}

					result.Append(pi.Name);
					valueQry.Append($"{_parameterPrefix}{pi.Name}");
					i++;
				}
			}

			result.AppendFormat(") VALUES ({0});", valueQry.ToString());
			result.Append("SELECT @@IDENTITY;");

			return result.ToString();
		}


		protected virtual string GetInsertAndQuery(E values)
		{
			return GetInsertQuery();
		}
		*/

		public static T ConvertFromDBVal<T>(object obj)
		{
			if (obj == null || obj == DBNull.Value)
			{
				return default(T); // returns the default value for the type
			}
			else
			{
				return (T)obj;
			}
		}

		public static string ColumnNameToPropertyName(string colName)
		{
			return colName.Replace("_", "").ToLower().FirstLetterToUpperCase();
		}
	}

	public enum RequestedColumns
	{
		ALL = 0,
		NO_EXCLUDED = 1,
		NO_PRIMARYKEY = 2,
		NO_COMPUTED = 4,
	}

}
