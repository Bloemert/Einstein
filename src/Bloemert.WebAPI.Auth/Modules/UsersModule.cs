﻿using Bloemert.Data.Core;
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
			ITwoWayMapper<User, UserModel> mapper,
			IUserIdentityProvider identityProvider)
			: base(appCfg, usersRepository, mapper, identityProvider, "/users")
		{

			Get("/login", args =>
			{
				if ( this.Context.CurrentUser == null || !this.Context.CurrentUser.Identity.IsAuthenticated)
				{ 
					return Negotiate
									.WithModel(new ModelWrapper<LoginModel> { Error = new ModelWrapperError { Message = "Login failed!" } })
									.WithStatusCode(HttpStatusCode.Forbidden);
				}

				User user = (User)this.Context.CurrentUser.Identity.AsUserIdentity().PersistentUser;

				// Login has it's own model!
				ModelWrapper<LoginModel> model = new ModelWrapper<LoginModel>
				{
					Data = new LoginModel
					{
						Id = user.Id,
						Name = user.Login,
						Admin = true
					}
				};

				return Negotiate
								.WithModel(model);
			});


			Put("/{id}/save", args =>
			{
				ModelWrapper<UserModel> model = this.Bind<ModelWrapper<UserModel>>();

				User entity = Mapper.Map(model.Data);

				if (!String.IsNullOrEmpty(model.Data.NewPassword))
				{
					entity.PasswordData = usersRepository.HashPassword(model.Data.NewPassword);
				}

				return Negotiate
								.WithModel(new ModelWrapper<UserModel> { Data = Mapper.Map(Repository.SaveEntity(entity)) });
			});

		}

	}
}
