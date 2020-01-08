using Einstein.DataAccessLayer.Core;

namespace Einstein.DataAccessLayer.Entities
{
  public class SkillCategory : BaseEntity
	{
		public virtual string Name { get; set; }

		public virtual string Description { get; set; }

	}
}
