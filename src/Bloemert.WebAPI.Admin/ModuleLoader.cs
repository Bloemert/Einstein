
using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Admin;
using Bloemert.Data.Entity.Admin.Entity;
using Bloemert.Lib.Auto.Mapping.AutoMapper.Autofac;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Admin.Models;
using Bloemert.WebAPI.Admin.Models.Mappers;
using Nancy;
using System;
using System.Reflection;

namespace Bloemert.WebAPI.Admin
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

			RegisterMappingProfiles(builder);
		}

		private void RegisterMappingProfiles(ContainerBuilder builder)
		{
			builder.RegisterTwoWayMappingProfile<LogMapperProfile, Log, LogModel>();

			builder.ConfigureAutoMapper();
		}

	}
}
