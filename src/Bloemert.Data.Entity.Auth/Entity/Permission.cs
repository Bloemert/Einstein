using Bloemert.Data.Core;

namespace Bloemert.Data.Entity.Auth
{
	public class Permission : BaseEntity
	{
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }


		public virtual ActionPermission ActionPermission { get; set; }
		public virtual Role Role { get; set; }
	}
}
