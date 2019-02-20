using Autofac;
using AutoMapper;
using Bloemert.Data.Core;
using Bloemert.Data.Core.Core;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Repository;
using Bloemert.Lib.Common;
using Bloemert.Lib.Config;
using Newtonsoft.Json;
using NHibernate;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Bloemert.Data.Entity.Auth.Repository.Implementation
{
	public class UserRepository : BaseRepository<UserRepository, User>, IUserRepository
	{

		public override string TableName { get; set; } = @"Users";

		public override Regex ExcludePropertyMatch { get; set; } = new Regex(@"^PasswordData$");

		public IAppConfig AppConfig { get; }

		public UserRepository(ILifetimeScope ioc,
									ISessionFactory sessionFactory,
									ILogger log,
									IAppConfig appConfig)
			: base(ioc, sessionFactory, log)
		{
			AppConfig = appConfig;
		}

		public User Validate(string login, string password)
		{
			string hashedPassword = this.HashPassword(password);

			return this.ListQuery(x => x.Active && x.Login.Equals(login) && x.PasswordData.Equals(hashedPassword)).FirstOrDefault();
		}

		public string HashPassword(string password)
		{
			if (String.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException("password");
			}

			// Keep your Password Salt in User/Environment specific config settings!
			string Salt = AppConfig.GetValue("PasswordSalt");
			System.Security.Cryptography.SHA512 hasher = System.Security.Cryptography.SHA512.Create();
			byte[] pwdHashed = hasher.ComputeHash(
													hasher.ComputeHash(
															hasher.ComputeHash(Encoding.UTF8.GetBytes(Salt + password))));

			return pwdHashed.ToHexString();
		}



		public override User SaveEntity(User entity)
		{
			Guid currentUserId = Guid.Empty;
			IUserIdentityProvider uip = IoC.Resolve<IUserIdentityProvider>();
			ClaimsPrincipal cp = uip.ClaimsPrincipal;
			if (cp != null && cp.Identity is IPersistentIdentity)
			{
				currentUserId = ((IPersistentIdentity)cp.Identity).PersistentUser.Id;
			}

			if (String.IsNullOrEmpty(entity.PasswordData))
			{
				// When no PasswordData is filled in by User, request it from DB first else the update will fail!
				User oldData = this.GetEntity(entity.Id);

				if ( oldData!=null )
				{
					entity.PasswordData = oldData.PasswordData;
				}
			}

			try
			{
				using (var session = SessionFactory.OpenSession())
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
								User newEntity = Mapper.Map<User>(entity);
								newEntity.EffectiveStartedOn = updateDate;
								newEntity.EffectiveStartedBy = currentUserId;
								newEntity.EffectiveModifiedOn = updateDate;
								newEntity.EffectiveModifiedBy = currentUserId;
								newEntity.EffectiveEndedOn = SqlDateTime.MaxValue.Value.AddSeconds(-1);
								newEntity.EffectiveEndedBy = Guid.Empty;

								newEntity = (User)session.Save(newEntity);
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
				Log.Error("User SaveEntity failed!", ex);

				throw ex;
			}

		}
	}
}
