using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Auth.Models;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloemert.WebAPI.Auth.Modules
{
	public class UsersModule : BaseModule<UsersModule, IUserRepository, User, UserModel>
	{

		public UsersModule(IAppConfig appCfg, IUserRepository usersRepository, 
			ITwoWayMapper<User, UserModel> mapper)
			: base(appCfg, usersRepository, mapper, "/users")
		{
		}
	}
}
