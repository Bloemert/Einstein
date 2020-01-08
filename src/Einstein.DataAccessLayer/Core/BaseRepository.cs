using Autofac;
using Einstein.DataAccessLayer.Mappings;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Einstein.DataAccessLayer.Core
{

  public class BaseRepository<E> : IRepository<E>
		where E : BaseEntity
  {
	 public virtual string TableName { get; set; }

	 public virtual string EntityTypeName { get; set; } = typeof(E).Name;

	 public virtual bool UseEffectiveVersioning { get; } = false;


	 protected ILifetimeScope IoC { get; }
	 protected EinsteinDbContext Db { get; }
	 protected ILogger Log { get; }


	 public BaseRepository(
								 ILifetimeScope ioc,
								 EinsteinDbContext db,
								 ILogger log)
	 {
		IoC = ioc;
		Db = db;
		Log = log;
	 }


	 protected System.Type BaseEntityType
	 {
		get { return typeof(E); }
	 }

	 protected Guid GetCurrentUserId()
	 {
		Guid currentUserId = Guid.Empty;
		IUserIdentityProvider uip = IoC.Resolve<IUserIdentityProvider>();
		ClaimsPrincipal cp = uip.ClaimsPrincipal;
		if (cp != null && cp.Identity is IPersistentIdentity)
		{
		  currentUserId = ((IPersistentIdentity)cp.Identity).PersistentUser.Id;
		}

		return currentUserId;
	 }


  public async virtual Task<E> NewEntityAsync()
	 {
		return await Task.Run<E>(() =>
	  {
		 E entity = (E)Activator.CreateInstance(BaseEntityType);
		 entity.EffectiveStartedBy = GetCurrentUserId();
		 entity.Id = Guid.NewGuid();

		 return entity;
	  });
	 }



	 public async virtual Task<E> GetEntityAsync(Guid id)
	 {
		return await Db.FindAsync<E>(new[] { id });
	 }


	 public virtual async Task<E> SaveEntityAsync(E entity)
	 {

		Guid currentUserId = GetCurrentUserId();

		try
		{
		  if (!entity.Id.Equals(Guid.Empty))
		  {
			 DateTime updateDate = DateTime.Now;
			 entity.EffectiveModifiedOn = updateDate;
			 entity.EffectiveModifiedBy = currentUserId;

			 if (UseEffectiveVersioning)
			 {
				// Set proper effective columns for Old record.
				entity.EffectiveEndedOn = updateDate;
				entity.EffectiveEndedBy = currentUserId;

				Db.Update(entity);

				// Save changed/modified entity as new record
				E newEntity = (E)Db.Entry(entity).CurrentValues.ToObject();
				newEntity.EffectiveStartedOn = updateDate;
				newEntity.EffectiveStartedBy = currentUserId;
				newEntity.EffectiveModifiedOn = updateDate;
				newEntity.EffectiveModifiedBy = currentUserId;
				newEntity.EffectiveEndedOn = SqlDateTime.MaxValue.Value.AddSeconds(-1);
				newEntity.EffectiveEndedBy = Guid.Empty;

				await Db.AddAsync(newEntity);
				await Db.SaveChangesAsync();

				return newEntity;
			 }
			 else
			 {
				Db.Update(entity);
				await Db.SaveChangesAsync();

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

			 entity.Id = Guid.NewGuid();
			 await Db.AddAsync(entity);
			 await Db.SaveChangesAsync();

			 return entity;
		  }
		}
		catch (Exception ex)
		{
		  Log.Error("BaseEntity for {FullName} SaveEntityAsync failed!", typeof(E).FullName, ex);

		  throw;
		}
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


	 public virtual async Task<bool> DeleteEntityAsync(E entity)
	 {
		if (entity != null)
		{
		  entity.EffectiveModifiedOn = entity.EffectiveEndedOn = DateTime.Now;
		  entity.EffectiveModifiedBy = entity.EffectiveEndedBy = GetCurrentUserId();
		  await SaveEntityAsync(entity);

		  return true;
		}
		return false;
	 }



	 public virtual async Task<E> FirstOrDefaultAsync(Expression<Func<E, bool>> predicate = null)
	 {
		E result;

		if (predicate != null)
		{
		  result = await Db.Set<E>().FirstOrDefaultAsync(predicate);
		}
		else
		{
		  result = await Db.Set<E>().FirstOrDefaultAsync();
		}

		return result;
	 }


	 public virtual async Task<IList<E>> ListQueryAsync(Expression<Func<E, bool>> predicate = null)
	 {
		IList<E> result;

		if (predicate != null)
		{
		  result = await Db.Set<E>().Where(predicate).ToListAsync();
		}
		else
		{
		  result = await Db.Set<E>().ToListAsync();
		}

		return result;
	 }
  }


}
