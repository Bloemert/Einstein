using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Repository;
using System;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Auth
{
  public class UserStore //: IUserStore<User, Guid>
  {
	 IUserRepository UserRepository { get; }

	 public UserStore(IUserRepository userRepository)
	 {
		UserRepository = userRepository;
	 }

	 public Task CreateAsync(User user)
	 {
		if (user == null)
		{
		  throw new ArgumentNullException("user");
		}

		UserRepository.SaveEntityAsync(user);

		return Task.FromResult<object>(null);
	 }

	 public Task UpdateAsync(User user)
	 {
		if (user == null)
		{
		  throw new ArgumentNullException("user");
		}

		UserRepository.SaveEntityAsync(user);

		return Task.FromResult<object>(null);
	 }

	 public Task DeleteAsync(User user)
	 {
		if (user == null)
		{
		  throw new ArgumentNullException("user");
		}

		UserRepository.DeleteEntityAsync(user);

		return Task.FromResult<object>(null);
	 }

	 public Task<User> FindByIdAsync(Guid userId)
	 {
		return UserRepository.FirstOrDefaultAsync(e => e.Id.Equals(userId));
	 }

	 public Task<User> FindByNameAsync(string userName)
	 {
		return UserRepository.FirstOrDefaultAsync(e => e.UserName.Equals(userName));
	 }


	 #region IDisposable Support
	 private bool disposedValue = false; // To detect redundant calls

	 protected virtual void Dispose(bool disposing)
	 {
		if (!disposedValue)
		{
		  disposedValue = true;
		}
	 }

	 // This code added to correctly implement the disposable pattern.
	 public void Dispose()
	 {
		// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		Dispose(true);
	 }
	 #endregion


  }
}
