using System.Security.Claims;
using System.Security.Principal;

namespace Einstein.WebAPI.Auth
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