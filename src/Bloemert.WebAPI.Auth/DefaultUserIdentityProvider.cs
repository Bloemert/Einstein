using Bloemert.Data.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Bloemert.WebAPI.Auth
{
	public class DefaultUserIdentityProvider : IUserIdentityProvider
	{
		public ClaimsPrincipal ClaimsPrincipal { get; set; }
	}
}
