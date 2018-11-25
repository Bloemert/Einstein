using Bloemert.Data.Core;
using System;

namespace Bloemert.Data.Entity.Skills.Entity
{
	public class SkillCategory : BaseEntity
	{
		public virtual string Name { get; set; }

		public virtual string Description { get; set; }

	}
}
