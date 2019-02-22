using Bloemert.Data.Core;
using Bloemert.Data.Entity.Work.Entity;
using Bloemert.Data.Entity.Work.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Work.Models;
using Serilog;

namespace Bloemert.WebAPI.Work.Modules
{
	public class EmployeeSkillModule : BaseModule<EmployeeSkillModule, IEmployeeSkillRepository, EmployeeSkill, EmployeeSkillModel>
	{

		public EmployeeSkillModule(IAppConfig appCfg, IEmployeeSkillRepository employeeSkillsRepository, 
			ITwoWayMapper<EmployeeSkill, EmployeeSkillModel> mapper,
			IUserIdentityProvider identityProvider,
			ILogger log)
			: base(appCfg, employeeSkillsRepository, mapper, identityProvider, log, "/work/employeeskills")
		{


		}

	}
}
