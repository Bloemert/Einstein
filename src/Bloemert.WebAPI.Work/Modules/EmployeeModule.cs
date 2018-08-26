using Bloemert.Data.Entity.Work;
using Bloemert.Data.Entity.Work.Entity;
using Bloemert.Data.Entity.Work.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Work.Models;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloemert.WebAPI.Work.Modules
{
	public class EmployeeModule : BaseModule<EmployeeModule, IEmployeeRepository, Employee, EmployeeModel>
	{

		public EmployeeModule(IAppConfig appCfg, IEmployeeRepository employeesRepository, 
			ITwoWayMapper<Employee, EmployeeModel> mapper)
			: base(appCfg, employeesRepository, mapper, "/work/employees")
		{

		}

	}
}
