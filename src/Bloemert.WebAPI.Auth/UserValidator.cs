using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Auth
{
	using System.Security.Claims;
	using System.Security.Principal;
	using Bloemert.Data.Entity.Auth.Entity;
	using Bloemert.Data.Entity.Auth.Repository;
	using Bloemert.WebAPI.Auth.Models;
	using Nancy.Authentication.Basic;

	public class UserValidator : IUserValidator
	{
		IUserRepository users { get; set; }

		public UserValidator(IUserRepository userRepository)
		{
			users = userRepository;
		}

		public ClaimsPrincipal Validate(string username, string password)
		{
			if ( String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) )
			{
				return null;
			}

			User user = users.Validate(username, password);
			if ( user != null )
			{
				return new UserPrincipal(new UserIdentity(user));
			}

			return null;
		}
	}
}
