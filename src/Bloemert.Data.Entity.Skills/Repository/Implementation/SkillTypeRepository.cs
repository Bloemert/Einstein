using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Common;
using Bloemert.Lib.Config;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bloemert.Data.Entity.Skills.Repository.Implementation
{
	public class SkillTypeRepository : BaseRepository<SkillTypeRepository, SkillType>, ISkillTypeRepository
	{

		public override string TableName { get; set; } = @"SkillTypes";

		public override Regex ExcludePropertyMatch { get; set; }

		public SkillTypeRepository(ICommonRepositoryDependencies crd)
			: base(crd)
		{
		}

	}
}
