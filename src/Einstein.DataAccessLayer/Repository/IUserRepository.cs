using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace Einstein.DataAccessLayer.Repository
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> ValidateAsync(string userName, string password);
		string HashPassword(string password);
	}
}
