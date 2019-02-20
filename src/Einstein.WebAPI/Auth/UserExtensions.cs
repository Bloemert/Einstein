using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Einstein.WebAPI.Auth
{
	public static class UserExtensions
	{

		public static UserIdentity AsUserIdentity(this IIdentity identity)
		{
			if (identity != null)
			{
				return identity as UserIdentity;
			}

			return null;
		}
	}
}
