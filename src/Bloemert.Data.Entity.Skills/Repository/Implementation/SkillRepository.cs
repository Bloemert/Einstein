using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Common;
using Bloemert.Lib.Config;
using NHibernate;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bloemert.Data.Entity.Skills.Repository.Implementation
{
	public class SkillRepository : BaseRepository<SkillRepository, Skill>, ISkillRepository
	{

		public override string TableName { get; set; } = @"Skills";

		public override Regex ExcludePropertyMatch { get; set; }

		public SkillRepository(ILifetimeScope ioc,
									ISessionFactory sessionFactory,
									ILogger log)
			: base(ioc, sessionFactory, log)
		{
		}

	}
}
