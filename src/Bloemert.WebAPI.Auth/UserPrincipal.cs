using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using Nancy.Security;
using System.Security.Claims;
using System.Security.Principal;

namespace Bloemert.WebAPI.Auth
{
	public class UserPrincipal : ClaimsPrincipal
	{
		public override IIdentity Identity { get; }

		public UserPrincipal(IIdentity identity) 
		{
			Identity = identity;
		}

	}
}