using Bloemert.Data.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Entity.Auth.Entity
{
    public class Session : BaseEntity
	{
			public int ActiveUserId { get; set; }

			public string ClientInfo { get; set; }

			public DateTime? Started { get; set; }
    }
}
