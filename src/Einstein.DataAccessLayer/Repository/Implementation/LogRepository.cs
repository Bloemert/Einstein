using Autofac;
using Bloemert.Common.Config;
using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Mappings;
using Serilog;

namespace Einstein.DataAccessLayer.Repository.Implementation
{
	public class LogRepository : BaseRepository<DBLog>, ILogRepository
	{

		public IAppConfig AppConfig { get; }

		public LogRepository(ILifetimeScope ioc,
									EinsteinDbContext db,
									ILogger log,
									IAppConfig appConfig)
			: base(ioc, db, log)
		{
			AppConfig = appConfig;
		}

	}
}
