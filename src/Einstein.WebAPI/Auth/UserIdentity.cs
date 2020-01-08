using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;

namespace Einstein.WebAPI.Auth
{
  public class UserIdentity : IPersistentIdentity
	{
		public IPersistentUser PersistentUser { get; }

		public string AuthenticationType => "Basic authentication";

		public bool IsAuthenticated => (PersistentUser != null);

		public string Name => PersistentUser.UserName;

		public UserIdentity(User user) 
		{
			PersistentUser = user;
		}

	}
}