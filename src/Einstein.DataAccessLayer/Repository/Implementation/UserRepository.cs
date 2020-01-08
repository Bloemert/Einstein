using Autofac;
using Bloemert.Common;
using Bloemert.Common.Config;
using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Mappings;
using Serilog;
using System;
using System.Data.SqlTypes;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.DataAccessLayer.Repository.Implementation
{
  public class UserRepository : BaseRepository<User>, IUserRepository
  {

	 public IAppConfig AppConfig { get; }

	 public UserRepository(ILifetimeScope ioc,
								 EinsteinDbContext db,
								 ILogger log,
								 IAppConfig appConfig)
		 : base(ioc, db, log)
	 {
		AppConfig = appConfig;
	 }

	 public async Task<User> ValidateAsync(string userName, string password)
	 {
		string hashedPassword = this.HashPassword(password);

		return await this.FirstOrDefaultAsync(x => x.Active && x.UserName.Equals(userName) && x.PasswordData.Equals(hashedPassword));
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

  }

}
