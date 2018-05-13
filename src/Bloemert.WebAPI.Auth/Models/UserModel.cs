using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Auth.Models
{

	public class UserModel : IModel
	{
		public int Id { get; set; }

		public bool Deleted { get; set; }


		public bool Active { get; set; }

		public string Login { get; set; }


		public string NewPassword { get; set; }


		public DateTime? ExpireDate { get; set; }
		public DateTime? LastLogin { get; set; }

		public int FailedAttempts { get; set; }
		public int GoodLogins { get; set; }
	}
}
