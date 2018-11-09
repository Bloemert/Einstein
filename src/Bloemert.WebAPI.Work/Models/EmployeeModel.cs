using Bloemert.Data.Core;
using Bloemert.Lib.WebAPI;
using Newtonsoft.Json;
using System;

namespace Bloemert.WebAPI.Work.Models
{
	public class EmployeeModel : IModel
	{
		public int Id { get; set; }

		public DateTime EffectiveStartedOn { get; set; }
		public int EffectiveStartedBy { get; set; }

		public DateTime EffectiveModifiedOn { get; set; }
		public int EffectiveModifiedBy { get; set; }

		public DateTime EffectiveEndedOn { get; set; }
		public int EffectiveEndedBy { get; set; }



		public int EmployeeNumber { get; set; }

		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }

		public DateTime? EmployedSince { get; set; }


		public int AvailabilityPerWeek { get; set; }

		public string FunctionLevel { get; set; }

		public string FunctionTitle { get; set; }

		public int? ManagerId { get; set; }
	}
}
