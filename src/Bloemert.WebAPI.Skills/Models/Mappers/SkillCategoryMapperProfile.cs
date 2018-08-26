using AutoMapper;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Repository;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Lib.Auto.Mapping.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Skills.Models.Mappers
{
	public class SkillCategoryMapperProfile : TwoWayMappingProfile<SkillCategory, SkillCategoryModel>
	{
		protected override void ConfigureMapping(IMappingExpression<SkillCategoryModel, SkillCategory> map)
		{
		}

		protected override void ConfigureMapping(IMappingExpression<SkillCategory, SkillCategoryModel> map)
		{
		}
	}
}
