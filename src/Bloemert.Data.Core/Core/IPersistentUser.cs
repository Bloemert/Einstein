using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Core.Core
{
	public interface IPersistentUser : IEntity
	{
		bool Active { get; set; }

		string Login { get; set; }

		DateTime? ExpireDate { get; set; }
		DateTime? LastLogin { get; set; }

		int FailedAttempts { get; set; }
		int GoodLogins { get; set; }


		string Firstname { get; set; }
		string Lastname { get; set; }
		string Email { get; set; }


	}
}
