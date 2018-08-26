using Bloemert.Data.Core;
using System;

namespace Bloemert.Data.Entity.Skills.Entity
{
	public class Skill : BaseEntity
	{
		public int Seqno { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }


		public string Version { get; set; }

		public int SkillTypeId { get; set; }

		public int SkillCategoryId { get; set; }

	}
}
