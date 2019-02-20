using Bloemert.Data.Core;
using Bloemert.Data.Entity.Admin;
using Bloemert.Data.Entity.Admin.Entity;
using Bloemert.Data.Entity.Admin.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.WebAPI.Admin.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloemert.WebAPI.Admin.Modules
{
	public class LogsModule : BaseModule<LogsModule, ILogRepository, DBLog>
	{

		public LogsModule(IAppConfig appCfg, ILogRepository logsRepository, 
			IUserIdentityProvider identityProvider,
			ILogger log)
			: base(appCfg, logsRepository, identityProvider, log)
		{

		}

	}
}
