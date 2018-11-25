using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Auth.Models
{

	public class UserModel : IModel
	{
		public Guid Id { get; set; }

		public DateTime EffectiveStartedOn { get; set; }
		public Guid EffectiveStartedBy { get; set; }

		public DateTime EffectiveModifiedOn { get; set; }
		public Guid EffectiveModifiedBy { get; set; }

		public DateTime EffectiveEndedOn { get; set; }
		public Guid EffectiveEndedBy { get; set; }



		public bool Active { get; set; }

		public string Login { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }


		public string NewPassword { get; set; }


		public DateTime? ExpireDate { get; set; }
		public DateTime? LastLogin { get; set; }

		public int FailedAttempts { get; set; }
		public int GoodLogins { get; set; }
	}
}
