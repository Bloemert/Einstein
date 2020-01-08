
using Autofac;
using Bloemert.Common.Config;
using Einstein.DataAccessLayer.Mappings;
using Microsoft.EntityFrameworkCore;
using System;

namespace Einstein.DataAccessLayer
{
  public class ModuleLoader : Autofac.Module
  {

	 protected override void Load(ContainerBuilder builder)
	 {
		// Register Entity Framework
		builder.Register<EinsteinDbContext>((c, p) =>
		{
		  string connString = c.Resolve<IAppConfig>().ConnectionString;
		  return new EinsteinDbContext(
					 new DbContextOptionsBuilder<EinsteinDbContext>()
							  .UseSqlServer(connString).Options);
		})
			 .InstancePerLifetimeScope();


		// Register all neccesary base types in proper order!
		builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
						 .Where(t => t.Name.EndsWith("Repository"))
						 .AsImplementedInterfaces();
	 }

  }
}
