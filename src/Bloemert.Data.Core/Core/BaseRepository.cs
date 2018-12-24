using Autofac;
using AutoMapper;
using Bloemert.Data.Core.Core;
using NHibernate;
using NHibernate.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
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

		ISessionFactory NDb { get; }
	}



	public class BaseRepository<R, E> : IRepository<E>
		where R : IRepository<E>
		where E : BaseEntity
	{
		public virtual string TableName { get; set; }

		public virtual string EntityTypeName { get; set; } = typeof(E).Name;

		public virtual Regex ExcludePropertyMatch { get; set; } = new Regex(@"^\s*$");

		public virtual bool UseEffectiveVersioning { get; } = false;

		public ICommonRepositoryDependencies Crd { get; }


		protected ILifetimeScope IoC { get { return Crd.IoC; } }
		protected ISessionFactory FNHSessionFactory { get { return Crd.NDb; } }
		protected ILogger Log { get { return Crd.Log; } }


		public BaseRepository(ICommonRepositoryDependencies crd)
		{
			Crd = crd;
		}


		protected System.Type BaseEntityType
		{
			get { return typeof(E); }
		}


		public virtual E NewEntity()
		{
			Guid currentUserId = Guid.Empty;
			IUserIdentityProvider uip = IoC.Resolve<IUserIdentityProvider>();
			ClaimsPrincipal cp = uip.ClaimsPrincipal;
			if (cp != null && cp.Identity is IPersistentIdentity)
			{
				currentUserId = ((IPersistentIdentity)cp.Identity).PersistentUser.Id;
			}

			E entity = (E)Activator.CreateInstance(BaseEntityType);
			entity.EffectiveStartedBy = currentUserId;
			entity.Id = Guid.NewGuid();

			return entity;
		}



		public virtual E GetEntity(Guid id)
		{
			E result = default(E);

			using (var session = Crd.NDb.OpenSession())
			{
				//using (var transaction = session.BeginTransaction())
				{
					result = session.Get<E>(id);
				}
			}

			return result;
		}

		public async virtual Task<E> GetEntityAsync(Guid id)
		{
			E result = default(E);

			using (var session = Crd.NDb.OpenSession())
			{
				//using (var transaction = session.BeginTransaction())
				{
					result = await session.GetAsync<E>(id);
				}
			}

			return result;
		}


		public virtual E SaveEntity(E entity)
		{
			Guid currentUserId = Guid.Empty;
			IUserIdentityProvider uip = IoC.Resolve<IUserIdentityProvider>();
			ClaimsPrincipal cp = uip.ClaimsPrincipal;
			if (cp != null && cp.Identity is IPersistentIdentity)
			{
				currentUserId = ((IPersistentIdentity)cp.Identity).PersistentUser.Id;
			}


			try
			{
				using (var session = FNHSessionFactory.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{

						if (entity.Id != null && !entity.Id.Equals(Guid.Empty))
						{
							DateTime updateDate = DateTime.Now;
							entity.EffectiveModifiedOn = updateDate;
							entity.EffectiveModifiedBy = currentUserId;

							if (UseEffectiveVersioning)
							{
								// Set proper effective columns for Old record.
								entity.EffectiveEndedOn = updateDate;
								entity.EffectiveEndedBy = currentUserId;

								session.Update(entity);

								// Save changed/modified entity as new record
								E newEntity = Mapper.Map<E>(entity);
								newEntity.EffectiveStartedOn = updateDate;
								newEntity.EffectiveStartedBy = currentUserId;
								newEntity.EffectiveModifiedOn = updateDate;
								newEntity.EffectiveModifiedBy = currentUserId;
								newEntity.EffectiveEndedOn = SqlDateTime.MaxValue.Value.AddSeconds(-1);
								newEntity.EffectiveEndedBy = Guid.Empty;

								newEntity = (E)session.Save(newEntity);
								transaction.Commit();

								return newEntity;
							}
							else
							{
								session.Update(entity);
								transaction.Commit();

								return entity;
							}
						}
						else
						{
							DateTime insertDate = DateTime.Now;
							entity.EffectiveStartedOn = insertDate;
							entity.EffectiveStartedBy = currentUserId;
							entity.EffectiveModifiedOn = insertDate;
							entity.EffectiveModifiedBy = currentUserId;
							entity.EffectiveEndedOn = SqlDateTime.MaxValue.Value.AddSeconds(-1);
							entity.EffectiveEndedBy = Guid.Empty;

							entity.Id = (Guid)session.Save(entity);
							transaction.Commit();

							return entity;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error("BaseEntity for {FullName} SaveEntity failed!", typeof(E).FullName, ex);

				throw ex;
			}
		}


		public virtual async Task<E> SaveEntityAsync(E entity)
		{
			E result = entity;

			using (var session = Crd.NDb.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					if (entity.Id == null)
					{
						result = (E)await session.SaveAsync(entity);
					}
					else
					{
						await session.UpdateAsync(entity);
					}

					transaction.Commit();
				}
			}

			return result;
		}



		public virtual bool DeleteEntity(Guid id)
		{
			var entity = GetEntity(id);
			if (entity != null)
			{
				return DeleteEntity(entity);
			}

			return false;
		}
		public virtual async Task<bool> DeleteEntityAsync(Guid id)
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




		public virtual IList<E> ListQuery(Expression<Func<E, bool>> predicate = null)
		{
			IList<E> result = default(IList<E>);

			try
			{
				using (var session = FNHSessionFactory.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						if (predicate != null)
						{
							result = session.Query<E>().Where(predicate).ToList();
						}
						else
						{
							result = session.Query<E>().ToList();
						}

						transaction.Commit();
					}
				}
			}
			catch ( Exception ex )
			{
				Log.Error("ListQuery failed on entity \"{FullName}\" ", typeof(E).FullName, ex);

				throw ex;
			}

			return result;
		}


		public virtual async Task<IList<E>> ListQueryAsync(Expression<Func<E, bool>> predicate = null)
		{
			IList<E> result = default(IList<E>);

			using (var session = FNHSessionFactory.OpenSession())
			{
				if (predicate != null)
				{
					result = await session.Query<E>().Where(predicate).ToListAsync();
				}
				else
				{
					result = await session.Query<E>().ToListAsync();
				}
			}

			return result;
		}
	}


}
