using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Bloemert.WebAPI.Auth
{
	public static class UserPrincipalExtensions
	{
		public static UserPrincipal AsPrincipal(this IPrincipal principal)
		{
			if (principal != null)
			{
				return principal as UserPrincipal
						?? new UserPrincipal(principal);
			}

			return null;
		}
	}
}
