﻿using Bloemert.Data.Core;
using Bloemert.Data.Core.Core;
using Bloemert.Data.Entity.Work.Entity;
using Newtonsoft.Json;
using System;

namespace Bloemert.Data.Entity.Auth.Entity
{
	public class User : BaseEntity, IPersistentUser
	{
		public virtual bool Active { get; set; }

		public virtual string Login { get; set; }

		// Only HashedPassword will be saved to DB
		// Don't expose this outside because then it will be visible in the REST/WebAPI!
		public virtual string PasswordData { get; set; }

		public virtual DateTime? ExpireDate { get; set; }
		public virtual DateTime? LastLogin { get; set; }

		public virtual int FailedAttempts { get; set; }
		public virtual int GoodLogins { get; set; }


		public virtual string Firstname { get; set; }
		public virtual string Lastname { get; set; }
		public virtual string Email { get; set; }


		public virtual Employee Employee { get; set; }

	}
}
