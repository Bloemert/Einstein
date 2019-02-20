
using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Lib.Auto.Mapping.AutoMapper.Autofac;
using Nancy;
using Nancy.Authentication.Basic;
using System;
using System.Reflection;

namespace Bloemert.WebAPI.Auth
{
	public class ModuleLoader : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			// Register referenced Modules
			builder.RegisterModule<Bloemert.Lib.Logging.ModuleLoader>();

			// Initialize and Register all neccesary base instances in proper order!
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
			 .Where(t => t.IsAssignableTo<INancyModule>())
			 .AsImplementedInterfaces()
			 .InstancePerRequest();

			builder.RegisterType<Bloemert.WebAPI.Auth.DefaultUserIdentityProvider>()
				.As<IUserIdentityProvider>()
				.SingleInstance()
				.InstancePerRequest();

			builder.RegisterType<UserValidator>()
				.As<IUserValidator>();


			RegisterMappingProfiles(builder);
		}

		private void RegisterMappingProfiles(ContainerBuilder builder)
		{
			builder.ConfigureAutoMapper();
		}

	}
}
