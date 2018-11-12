using Bloemert.Data.Core;
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
	public class EmployeeSkillModule : BaseModule<EmployeeSkillModule, IEmployeeSkillRepository, EmployeeSkill, EmployeeSkillModel>
	{

		public EmployeeSkillModule(IAppConfig appCfg, IEmployeeSkillRepository employeeSkillsRepository, 
			ITwoWayMapper<EmployeeSkill, EmployeeSkillModel> mapper,
			IUserIdentityProvider identityProvider)
			: base(appCfg, employeeSkillsRepository, mapper, identityProvider, "/work/employeeskills")
		{


		}

	}
}
