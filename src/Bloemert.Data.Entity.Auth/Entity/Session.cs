using Bloemert.Data.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Entity.Auth.Entity
{
    public class Session : BaseEntity
	{
			public virtual int ActiveUserId { get; set; }

			public virtual string ClientInfo { get; set; }

			public virtual DateTime? Started { get; set; }
    }
}
