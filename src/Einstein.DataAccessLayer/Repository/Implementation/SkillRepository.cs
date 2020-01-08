using Autofac;
using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Mappings;
using Serilog;

namespace Einstein.DataAccessLayer.Repository.Implementation
{
	public class SkillRepository : BaseRepository<Skill>, ISkillRepository
	{

		public SkillRepository(ILifetimeScope ioc,
									EinsteinDbContext db,
									ILogger log)
			: base(ioc, db, log)
	 {
		}

	}
}
