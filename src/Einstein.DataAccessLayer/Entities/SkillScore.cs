using Einstein.DataAccessLayer.Core;

namespace Einstein.DataAccessLayer.Entities
{
  public class SkillScore : BaseEntity
	{
		public virtual string Name { get; set; }

		public virtual string Description { get; set; }

		public virtual int Value { get; set; }
	}
}
