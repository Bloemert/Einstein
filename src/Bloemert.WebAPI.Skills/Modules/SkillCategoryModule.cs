using Bloemert.Data.Core;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Skills.Models;
using Serilog;

namespace Bloemert.WebAPI.Skills.Modules
{
	public class SkillCategoryModule : BaseModule<SkillCategoryModule, ISkillCategoryRepository, SkillCategory, SkillCategoryModel>
	{

		public SkillCategoryModule(IAppConfig appCfg, ISkillCategoryRepository skillCategoryRepository, 
			ITwoWayMapper<SkillCategory, SkillCategoryModel> mapper,
			IUserIdentityProvider identityProvider,
			ILogger log)
			: base(appCfg, skillCategoryRepository, mapper, identityProvider, log, "/skills/categories")
		{

		}

	}
}
