using System.Security.Claims;

namespace Einstein.DataAccessLayer.Core
{
  public interface IUserIdentityProvider
	{
		ClaimsPrincipal ClaimsPrincipal { get; set; }
	}
}
