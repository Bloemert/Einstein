using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using System;
using System.Xml;

namespace Bloemert.Data.Entity.Admin.Entity
{
	public class Log : BaseEntity
	{
		public virtual string Message { get; set; }

   public virtual string Level { get; set; }

		public virtual DateTimeOffset TimeStamp { get; set; }

		public virtual string Exception { get; set; }

		//public virtual XmlDocument Properties { get; set; }

		public virtual string LogEvent { get; set; }

	}
}
