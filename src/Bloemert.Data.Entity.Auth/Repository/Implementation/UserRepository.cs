using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Core.Core;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Repository;
using Bloemert.Lib.Common;
using Bloemert.Lib.Config;
using Newtonsoft.Json;
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

		public UserRepository(ICommonRepositoryDependencies crd)
			: base(crd)
		{
			AppConfig = crd.IoC.Resolve<IAppConfig>();
		}

		public User Validate(string login, string password)
		{
			string hashedPassword = this.HashPassword(password);

			return this.ListEntity<User>(@"SELECT * FROM Users WHERE Login = @Login AND PasswordData = @HPwd", new { Login = login, HPwd = hashedPassword }).FirstOrDefault();
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
			IDbParameters dbParameters = DbParameters.Create(entity);
			IList<string> excludedColumns = null;

			int currentUserId = -1;
			IUserIdentityProvider uip = IoC.Resolve<IUserIdentityProvider>();
			ClaimsPrincipal cp = uip.ClaimsPrincipal;
			if (cp != null && cp.Identity is IPersistentIdentity)
			{
				currentUserId = ((IPersistentIdentity)cp.Identity).PersistentUser.Id;
			}

			if (String.IsNullOrEmpty(entity.PasswordData))
			{
				excludedColumns = new List<string> { "PasswordData" };
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

					if ( result < 1 )
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

					return Db.ExecuteAndQuery<User>(QueryTemplate.CreateInsertQuery(excludedColumns), DbParameters.Create(entity));
				}
				else
				{
					return this.Db.ExecuteAndQuery<User>(QueryTemplate.CreateUpdateQuery(excludedColumns), dbParameters);
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

				return Db.ExecuteAndQuery<User>(QueryTemplate.CreateInsertQuery(excludedColumns), dbParameters);
			}
		}

	}
}
