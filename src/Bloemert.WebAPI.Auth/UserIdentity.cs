using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using Nancy.Security;
using System.Security.Claims;
using System.Security.Principal;

namespace Bloemert.WebAPI.Auth
{
	public class UserIdentity : IIdentity
	{
		public User PersistentUser { get; }

		public string AuthenticationType => "Basic authentication";

		public bool IsAuthenticated => (PersistentUser != null);

		public string Name => PersistentUser.Login;

		public UserIdentity(User user) 
		{
			PersistentUser = user;
		}

	}
}