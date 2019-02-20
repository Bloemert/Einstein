using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Auth
{
	using System.Security.Claims;
	using System.Security.Principal;
	using Bloemert.Data.Entity.Auth.Entity;
	using Bloemert.Data.Entity.Auth.Repository;
	using Nancy.Authentication.Basic;
	using Serilog;

	public class UserValidator : IUserValidator
	{
		private ILogger Log { get; }

		IUserRepository users { get; set; }

		public UserValidator(ILogger log, IUserRepository userRepository)
		{
			Log = log;
			users = userRepository;
		}

		public ClaimsPrincipal Validate(string username, string password)
		{
			if ( String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) )
			{
				Log.Warning("Login validation failed because of wrong input: {username}!", username);

				return null;
			}

			User user = users.Validate(username, password);
			if ( user != null )
			{
				Log.Information("Login validation OK for: {username}!", username);

				return new UserPrincipal(new UserIdentity(user));
			}
			else
			{
				Log.Warning("Login validation failed because of wrong username / password combination: {username}!", username);
			}

			return null;
		}
	}
}
