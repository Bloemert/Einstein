
using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Mappings;
using FluentNHibernate.Automapping;
using System;
using System.Reflection;

namespace Bloemert.Data.Entity.Auth
{
	public class ModuleLoader : Autofac.Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			// Register all neccesary base types in proper order!
			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
						 .Where(t => t.Name.EndsWith("Repository"))
						 .AsImplementedInterfaces();
		}

	}
}
