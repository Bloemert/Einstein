using Autofac;
using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Mappings;
using Serilog;

namespace Einstein.DataAccessLayer.Repository.Implementation
{
	public class SpotRepository : BaseRepository<Spot>, ISpotRepository
	{
		public SpotRepository(ILifetimeScope ioc,
									EinsteinDbContext db,
									ILogger log)
			: base(ioc, db, log)
	 {
		}

	}
}
