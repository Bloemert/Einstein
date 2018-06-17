
using Autofac;
using Bloemert.Data.Core;
using System;
using System.Reflection;

namespace Bloemert.Data.Entity.Skills
{
	public class ModuleLoader : Autofac.Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			// Register all neccesary base types in proper order!
			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
						 .Where(t => t.Name.EndsWith("Repository"))
						 .AsImplementedInterfaces()
			;
		}

	}
}
