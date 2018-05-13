using Bloemert.Data.Core;

namespace Bloemert.Data.Entity.Auth
{
	public class ActionPermission : BaseEntity
	{

		// Association(s)
		public virtual BaseEntity BaseEntity { get; set; }
		public virtual Action Action { get; set; }
		public virtual Permission Permission { get; set; }
		
	}
}
