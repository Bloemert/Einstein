using Bloemert.Data.Core.Core;
using Bloemert.Data.Entity.Auth.Entity;

namespace Einstein.WebAPI.Auth
{
	public class UserIdentity : IPersistentIdentity
	{
		public IPersistentUser PersistentUser { get; }

		public string AuthenticationType => "Basic authentication";

		public bool IsAuthenticated => (PersistentUser != null);

		public string Name => PersistentUser.Login;

		public UserIdentity(User user) 
		{
			PersistentUser = user;
		}

	}
}