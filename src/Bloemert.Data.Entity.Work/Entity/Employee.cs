using Bloemert.Data.Core;
using Newtonsoft.Json;
using System;

namespace Bloemert.Data.Entity.Work.Entity
{
	public class Employee : BaseEntity
	{
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }


		public DateTime? EmployedSince { get; set; }

		public int EmployeeNumber { get; set; }

		public int AvailabilityPerWeek { get; set; }

		public string FunctionLevel { get; set; }

		public string FunctionTitle { get; set; }

		public int? ManagerId { get; set; }
	}
}
