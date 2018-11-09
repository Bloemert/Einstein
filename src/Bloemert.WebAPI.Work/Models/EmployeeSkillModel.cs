using Bloemert.Data.Core;
using Bloemert.Lib.WebAPI;
using Newtonsoft.Json;
using System;

namespace Bloemert.WebAPI.Work.Models
{
	public class EmployeeSkillModel : IModel
	{
		public int Id { get; set; }

		public DateTime EffectiveStartedOn { get; set; }
		public int EffectiveStartedBy { get; set; }

		public DateTime EffectiveModifiedOn { get; set; }
		public int EffectiveModifiedBy { get; set; }

		public DateTime EffectiveEndedOn { get; set; }
		public int EffectiveEndedBy { get; set; }


		public int EmployeeId { get; set; }

		public int SkillId { get; set; }

		public int ScoreId { get; set; }

	}
}
