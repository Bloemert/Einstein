using Bloemert.Data.Core;
using Bloemert.Data.Core.Core;
using Newtonsoft.Json;
using System;

namespace Bloemert.Data.Entity.Auth.Entity
{
	public class User : BaseEntity, IPersistentUser
	{
		public bool Active { get; set; }

		public string Login { get; set; }

		// Only HashedPassword will be saved to DB
		// Don't expose this outside because then it will be visible in the REST/WebAPI!
		public string PasswordData { get; set; }

		public DateTime? ExpireDate { get; set; }
		public DateTime? LastLogin { get; set; }

		public int FailedAttempts { get; set; }
		public int GoodLogins { get; set; }


		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }

	}
}
