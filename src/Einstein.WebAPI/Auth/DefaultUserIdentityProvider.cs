using Einstein.DataAccessLayer.Core;
using System.Security.Claims;

namespace Einstein.WebAPI.Auth
{
  public class DefaultUserIdentityProvider : IUserIdentityProvider
	{
		public ClaimsPrincipal ClaimsPrincipal { get; set; }
	}
}
