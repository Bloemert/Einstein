using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using Nancy.Security;
using System.Security.Claims;
using System.Security.Principal;

namespace Bloemert.WebAPI.Auth
{
	public class UserPrincipal : ClaimsPrincipal
	{
		protected User PersistentUser { get; set; }

		public UserPrincipal(IPrincipal principal) : base(principal)
		{
		}

		public UserPrincipal(User user) 
		{
			PersistentUser = user;
		}


		public string FullName => FindFirst(ClaimTypes.Name).Value;

		public string EMail => FindFirst(ClaimTypes.Email).Value;

	}
}