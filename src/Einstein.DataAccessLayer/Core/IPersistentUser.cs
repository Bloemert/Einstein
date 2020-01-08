using System;

namespace Einstein.DataAccessLayer.Core
{
  public interface IPersistentUser : IEntity
	{
		bool Active { get; set; }

		string UserName { get; set; }

		DateTime? ExpireDate { get; set; }
		DateTime? LastLogin { get; set; }

		int FailedAttempts { get; set; }
		int GoodLogins { get; set; }


		string Firstname { get; set; }
		string Lastname { get; set; }
		string Email { get; set; }


	}
}
