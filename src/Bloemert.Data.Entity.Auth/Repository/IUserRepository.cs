using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth.Entity;

namespace Bloemert.Data.Entity.Auth.Repository
{
	public interface IUserRepository : IRepository<User>
	{
		User Validate(string login, string password);
		string HashPassword(string password);
	}
}
