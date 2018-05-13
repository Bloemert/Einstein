using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth.Entity;
using System.Collections.Generic;

namespace Bloemert.Data.Entity.Auth
{
	public class Role : BaseEntity
	{
		public virtual string Name { get; set; }

		public virtual User User { get; set; }

		public virtual IList<Permission> Permissions { get; set; }
	}
}
