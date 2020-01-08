using Einstein.DataAccessLayer.Core;
using System;

namespace Einstein.DataAccessLayer.Entities
{
  public class Session : BaseEntity
	{
		public virtual User CurrentUser { get; set; }

		public virtual string ClientInfo { get; set; }

		public virtual DateTime? Started { get; set; }
	}
}
