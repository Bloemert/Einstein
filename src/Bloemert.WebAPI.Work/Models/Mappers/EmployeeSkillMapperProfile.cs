using AutoMapper;
using Bloemert.Data.Entity.Work.Entity;
using Bloemert.Data.Entity.Work.Repository;
using Bloemert.Lib.Auto.Mapping.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Work.Models.Mappers
{
	public class EmployeeSkillMapperProfile : TwoWayMappingProfile<EmployeeSkill, EmployeeSkillModel>
	{
		public IEmployeeRepository EmployeeSkills { get; set; }

		protected override void ConfigureMapping(IMappingExpression<EmployeeSkillModel, EmployeeSkill> map)
		{
		}

		protected override void ConfigureMapping(IMappingExpression<EmployeeSkill, EmployeeSkillModel> map)
		{
		}
	}
}
