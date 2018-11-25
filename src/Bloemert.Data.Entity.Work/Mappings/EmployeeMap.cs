using Bloemert.Data.Core.Core;
using Bloemert.Data.Entity.Work.Entity;

namespace Bloemert.Data.Entity.Auth.Mappings
{
	public class EmployeeMap : BaseMap<Employee>
	{
		public EmployeeMap()
		{
			Table("Employees");

			Map(x => x.Firstname);
			Map(x => x.Lastname);
			Map(x => x.Email);

			Map(x => x.EmployedSince);
			Map(x => x.EmployeeNumber);
			Map(x => x.AvailabilityPerWeek);
			Map(x => x.FunctionLevel);
			Map(x => x.FunctionTitle);

			References(x => x.Manager).Column("ManagerId");
		}
	}
}
