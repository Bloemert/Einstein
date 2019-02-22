
using Bloemert.Data.Core;

namespace Bloemert.Data.Entity.Skills.Entity
{
	public class SkillScore : BaseEntity
	{
		public virtual string Name { get; set; }

		public virtual string Description { get; set; }

		public virtual int Value { get; set; }
	}
}
