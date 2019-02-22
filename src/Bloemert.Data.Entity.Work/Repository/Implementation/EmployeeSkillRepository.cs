using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Work.Entity;
using Bloemert.Lib.Common;
using Bloemert.Lib.Config;
using Newtonsoft.Json;
using NHibernate;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bloemert.Data.Entity.Work.Repository.Implementation
{
	public class EmployeeSkillRepository : BaseRepository<EmployeeSkillRepository, EmployeeSkill>, IEmployeeSkillRepository
	{

		public override string TableName { get; set; } = @"EmployeeSkills";


		public EmployeeSkillRepository(ILifetimeScope ioc,
									ISessionFactory sessionFactory,
									ILogger log)
			: base(ioc, sessionFactory, log)
		{
		}
	}
}
