
using Autofac;
using Einstein.DataAccessLayer.Core;
using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Mappings;
using Serilog;

namespace Einstein.DataAccessLayer.Repository.Implementation
{
	public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
	{

		public EmployeeRepository(
									ILifetimeScope ioc,
									EinsteinDbContext db,
									ILogger log)
			: base(ioc, db, log)
		{
		}
	}
}
