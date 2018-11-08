using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using System;
using System.Xml;

namespace Bloemert.Data.Entity.Admin.Entity
{
	public class Log : BaseEntity
	{
		public string Message { get; set; }

   public string Level { get; set; }

		public DateTimeOffset TimeStamp { get; set; }

		public string Exception { get; set; }

		public XmlDocument Properties { get; set; }

		public string LogEvent { get; set; }

	}
}
