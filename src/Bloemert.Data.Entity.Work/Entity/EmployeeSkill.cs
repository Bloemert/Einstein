using Bloemert.Data.Core;
using Newtonsoft.Json;
using System;

namespace Bloemert.Data.Entity.Work.Entity
{
	public class EmployeeSkill : BaseEntity
	{
		public virtual int EmployeeId { get; set; }

		public virtual int SkillId { get; set; }

		public virtual int ScoreId { get; set; }

	}
}
