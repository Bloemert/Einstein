using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using System;

namespace Bloemert.Data.Entity.Admin
{
	public class AuditLog : BaseEntity
	{
		public virtual string Message { get; set; }
		public virtual int Level { get; set; }
		public virtual DateTime TimeStamp { get; set; }

		// Association(s)
		public virtual User User { get; set; }

		public virtual BaseEntity BeforeEntity { get; set; }
		public virtual BaseEntity AfterEntity { get; set; }

	}
}
