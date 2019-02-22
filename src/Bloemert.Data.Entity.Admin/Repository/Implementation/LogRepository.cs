using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Admin.Entity;
using Bloemert.Data.Entity.Admin.Repository;
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

namespace Bloemert.Data.Entity.Admin.Repository.Implementation
{
	public class LogRepository : BaseRepository<LogRepository, Entity.DBLog>, ILogRepository
	{

		public override string TableName { get; set; } = @"Logs";

		public override Regex ExcludePropertyMatch { get; set; } = new Regex(@"^PasswordData$");

		public IAppConfig AppConfig { get; }

		public LogRepository(ILifetimeScope ioc,
									ISessionFactory sessionFactory,
									ILogger log,
									IAppConfig appConfig)
			: base(ioc, sessionFactory, log)
		{
			AppConfig = appConfig;
		}

	}
}
