﻿using Autofac;
using Bloemert.Data.Core.Core;
using Bloemert.Data.Core.Templates;
using Bloemert.Lib.Common;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Claims;
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

		public virtual bool UseEffectiveVersioning { get; } = false;

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

			int currentUserId = -1;
			IUserIdentityProvider uip = IoC.Resolve<IUserIdentityProvider>();
			ClaimsPrincipal cp = uip.ClaimsPrincipal;
			if (cp != null && cp.Identity is IPersistentIdentity)
			{
				currentUserId = ((IPersistentIdentity)cp.Identity).PersistentUser.Id;
			}

			if (entity.Id > 0)
			{
				DateTime updateDate = DateTime.Now;
				dbParameters.AddInputParameter("EffectiveModifiedOn", updateDate, DbType.DateTime, null);
				dbParameters.AddInputParameter("EffectiveModifiedBy", currentUserId, DbType.Int32, null);

				if (UseEffectiveVersioning)
				{
					// Set proper effective columns for Old record.
					dbParameters.AddInputParameter("EffectiveEndedOn", updateDate, DbType.DateTime, null);
					dbParameters.AddInputParameter("EffectiveEndedBy", currentUserId, DbType.Int32, null);
					int result = this.Db.Execute($"UPDATE {TableName} " +
																$"SET EffectiveModifiedOn = @EffectiveModifiedOn," +
																$"		EffectiveModifiedBy = @EffectiveModifiedBy," +
																$"		EffectiveEndedOn = @EffectiveEndedOn, " +
																$"		EffectiveEndedBy = @EffectiveEndedBy " +
																$"WHERE Id = @Id AND EffectiveEndedOn > GetDate()", dbParameters);

					if (result < 1)
					{
						Log.Error("UserRepository.SaveEntity failed: No update done during effectiveVersioning!");

						throw new ApplicationException("UserRepository.SaveEntity failed: No update done during effectiveVersioning!");
					}

					// Save changed/modified entity as new record
					entity.EffectiveStartedOn = updateDate;
					entity.EffectiveStartedBy = currentUserId;
					entity.EffectiveModifiedOn = updateDate;
					entity.EffectiveModifiedBy = currentUserId;
					entity.EffectiveEndedOn = SqlDateTime.MaxValue.Value.AddSeconds(-1);
					entity.EffectiveEndedBy = -1;

					return Db.ExecuteAndQuery<E>(QueryTemplate.CreateInsertQuery(), DbParameters.Create(entity));
				}
				else
				{
					return this.Db.ExecuteAndQuery<E>(QueryTemplate.CreateUpdateQuery(), dbParameters);
				}
			}
			else
			{
				DateTime insertDate = DateTime.Now;
				dbParameters.AddInputParameter("EffectiveStartedOn", insertDate, DbType.DateTime, null);
				dbParameters.AddInputParameter("EffectiveStartedBy", currentUserId, DbType.Int32, null);
				dbParameters.AddInputParameter("EffectiveModifiedOn", insertDate, DbType.DateTime, null);
				dbParameters.AddInputParameter("EffectiveModifiedBy", currentUserId, DbType.Int32, null);
				dbParameters.AddInputParameter("EffectiveEndedOn", SqlDateTime.MaxValue.Value.AddSeconds(-1), DbType.DateTime, null);
				dbParameters.AddInputParameter("EffectiveEndedBy", -1, DbType.Int32, null);

				return Db.ExecuteAndQuery<E>(QueryTemplate.CreateInsertQuery(), dbParameters);
			}
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
				entity.EffectiveEndedOn = DateTime.Now;
				//TODO: entity.EffectiveEndedBy

				SaveEntity(entity);

				return true;
			}
			return false;
		}

		public virtual async Task<bool> DeleteEntityAsync(E entity)
		{
			if (entity != null)
			{
				entity.EffectiveModifiedOn = entity.EffectiveEndedOn = DateTime.Now;
				// TODO: entity.EffectiveModifiedBy = entity.EffectiveEndedBy = CURRENT USER!
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
