
using Autofac;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Lib.Auto.Mapping.AutoMapper.Autofac;
using Bloemert.WebAPI.Skills.Models;
using Bloemert.WebAPI.Skills.Models.Mappers;
using Nancy;
using System.Reflection;

namespace Bloemert.WebAPI.Skills
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
			builder.RegisterTwoWayMappingProfile<SpotMapperProfile, Spot, SpotModel>();
			builder.RegisterTwoWayMappingProfile<SectorMapperProfile, Sector, SectorModel>();
			builder.RegisterTwoWayMappingProfile<RingMapperProfile, Ring, RingModel>();

			builder.RegisterTwoWayMappingProfile<SkillMapperProfile, Skill, SkillModel>();
			builder.RegisterTwoWayMappingProfile<SkillTypeMapperProfile, SkillType, SkillTypeModel>();
			builder.RegisterTwoWayMappingProfile<SkillCategoryMapperProfile, SkillCategory, SkillCategoryModel>();

			builder.ConfigureAutoMapper();
		}

	}
}
