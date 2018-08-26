using AutoMapper;
using Bloemert.Data.Entity.Work.Entity;
using Bloemert.Data.Entity.Work.Repository;
using Bloemert.Lib.Auto.Mapping.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Work.Models.Mappers
{
	public class EmployeeMapperProfile : TwoWayMappingProfile<Employee, EmployeeModel>
	{
		public IEmployeeRepository Employees { get; set; }

		protected override void ConfigureMapping(IMappingExpression<EmployeeModel, Employee> map)
		{
		}

		protected override void ConfigureMapping(IMappingExpression<Employee, EmployeeModel> map)
		{
		}
	}
}
