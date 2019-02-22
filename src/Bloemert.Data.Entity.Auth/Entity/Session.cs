using Bloemert.Data.Core;
using System;

namespace Bloemert.Data.Entity.Auth.Entity
{
	public class Session : BaseEntity
	{
		public virtual User CurrentUser { get; set; }

		public virtual string ClientInfo { get; set; }

		public virtual DateTime? Started { get; set; }
	}
}
