using Bloemert.Data.Core;
using Newtonsoft.Json;
using System;

namespace Bloemert.Data.Entity.Work.Entity
{
	public class EmployeeSkill : BaseEntity
	{
		public int EmployeeId { get; set; }

		public int SkillId { get; set; }

		public int ScoreId { get; set; }

	}
}
