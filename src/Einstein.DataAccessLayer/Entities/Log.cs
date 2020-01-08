using Einstein.DataAccessLayer.Core;
using System;

namespace Einstein.DataAccessLayer.Entities
{
  public class DBLog : BaseEntity
	{
		public virtual string Message { get; set; }

   public virtual string Level { get; set; }

		public virtual DateTimeOffset TimeStamp { get; set; }

		public virtual string Exception { get; set; }

		//public virtual XmlDocument Properties { get; set; }

		public virtual string LogEvent { get; set; }

	}
}
