using Einstein.DataAccessLayer.Core;

namespace Einstein.DataAccessLayer.Entities
{
  public class Role : BaseEntity
	{
		public virtual string Name { get; set; }

		public virtual User User { get; set; }

	}
}
