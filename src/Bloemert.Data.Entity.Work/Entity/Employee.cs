using Bloemert.Data.Core;
using Newtonsoft.Json;
using System;

namespace Bloemert.Data.Entity.Work.Entity
{
	public class Employee : BaseEntity
	{
		public virtual string Firstname { get; set; }
		public virtual string Lastname { get; set; }
		public virtual string Email { get; set; }


		public virtual DateTime? EmployedSince { get; set; }

		public virtual int EmployeeNumber { get; set; }

		public virtual int AvailabilityPerWeek { get; set; }

		public virtual string FunctionLevel { get; set; }

		public virtual string FunctionTitle { get; set; }

		public virtual Employee Manager { get; set; }
	}
}
