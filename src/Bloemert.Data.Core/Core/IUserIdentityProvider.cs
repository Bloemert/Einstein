using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Bloemert.Data.Core
{
	public interface IUserIdentityProvider
	{
		ClaimsPrincipal ClaimsPrincipal { get; set; }
	}
}
