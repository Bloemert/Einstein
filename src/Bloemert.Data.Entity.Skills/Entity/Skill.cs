using Bloemert.Data.Core;
using System;

namespace Bloemert.Data.Entity.Skills.Entity
{
	public class Skill : BaseEntity
	{
		public virtual int Seqno { get; set; }

		public virtual string Name { get; set; }

		public virtual string Description { get; set; }


		public virtual string Version { get; set; }

		public virtual int SkillTypeId { get; set; }

		public virtual int SkillCategoryId { get; set; }

	}
}
