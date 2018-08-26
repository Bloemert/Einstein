
using Bloemert.Data.Core;

namespace Bloemert.Data.Entity.Skills.Entity
{
	public class SkillScore : BaseEntity
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int Value { get; set; }
	}
}
