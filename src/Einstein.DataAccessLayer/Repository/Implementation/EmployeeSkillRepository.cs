using Autofac;
using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Mappings;
using Serilog;

namespace Einstein.DataAccessLayer.Repository.Implementation
{
	public class EmployeeSkillRepository : BaseRepository<EmployeeSkill>, IEmployeeSkillRepository
	{

		public EmployeeSkillRepository(
									ILifetimeScope ioc,
									EinsteinDbContext db,
									ILogger log)
			: base(ioc, db, log)
		{
		}
	}
}
