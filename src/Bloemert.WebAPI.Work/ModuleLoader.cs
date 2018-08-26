
using Autofac;
using Bloemert.Data.Entity.Work.Entity;
using Bloemert.Lib.Auto.Mapping.AutoMapper.Autofac;
using Bloemert.WebAPI.Work.Models;
using Bloemert.WebAPI.Work.Models.Mappers;
using Nancy;
using System.Reflection;

namespace Bloemert.WebAPI.Work
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
			builder.RegisterTwoWayMappingProfile<EmployeeMapperProfile, Employee, EmployeeModel>();
			builder.RegisterTwoWayMappingProfile<EmployeeSkillMapperProfile, EmployeeSkill, EmployeeSkillModel>();

			builder.ConfigureAutoMapper();
		}

	}
}
