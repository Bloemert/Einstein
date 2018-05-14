using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Repository;
using Bloemert.Lib.Common;
using Bloemert.Lib.Config;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
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


		public override IList<string> GetColumnsFromMetaData(RequestedColumns cols = RequestedColumns.ALL)
		{
			return (from ci in QueryTemplate.EntityMetadata
							where (cols == RequestedColumns.ALL ||
											(!ci.IsExcludedProperty || !cols.HasFlag(RequestedColumns.NO_EXCLUDED)) &&
											(!ci.IsIdentity || !cols.HasFlag(RequestedColumns.NO_PRIMARYKEY)) &&
											(!ci.IsComputed || !cols.HasFlag(RequestedColumns.NO_COMPUTED)))
							select ci.ColumnName)
							.Except(new string[] { "PasswordData"})
							.ToList();
		}

	}
}
