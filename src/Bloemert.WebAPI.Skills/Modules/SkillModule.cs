﻿using Bloemert.Data.Core;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Skills.Models;
using Serilog;

namespace Bloemert.WebAPI.Skills.Modules
{
	public class SkillModule : BaseModule<SkillModule, ISkillRepository, Skill, SkillModel>
	{

		public SkillModule(IAppConfig appCfg, ISkillRepository skillRepository, 
			ITwoWayMapper<Skill, SkillModel> mapper,
			IUserIdentityProvider identityProvider,
			ILogger log)
			: base(appCfg, skillRepository, mapper, identityProvider, log, "/skills/skills")
		{

		}

	}
}
