using Bloemert.Data.Entity.Admin;
using Bloemert.Data.Entity.Admin.Entity;
using Bloemert.Data.Entity.Admin.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Admin.Models;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloemert.WebAPI.Admin.Modules
{
	public class LogsModule : BaseModule<LogsModule, ILogRepository, Log, LogModel>
	{

		public LogsModule(IAppConfig appCfg, ILogRepository logsRepository, 
			ITwoWayMapper<Log, LogModel> mapper)
			: base(appCfg, logsRepository, mapper, "/logs")
		{

		}

	}
}
