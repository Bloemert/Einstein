﻿using Bloemert.Data.Core;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Skills.Models;

namespace Bloemert.WebAPI.Skills.Modules
{
	public class SkillTypeModule : BaseModule<SkillTypeModule, ISkillTypeRepository, SkillType, SkillTypeModel>
	{

		public SkillTypeModule(IAppConfig appCfg, ISkillTypeRepository skillTypeRepository, 
			ITwoWayMapper<SkillType, SkillTypeModel> mapper,
			IUserIdentityProvider identityProvider)
			: base(appCfg, skillTypeRepository, mapper, identityProvider, "/skills/types")
		{

		}

	}
}
