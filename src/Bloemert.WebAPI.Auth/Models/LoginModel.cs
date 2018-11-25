using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Auth.Models
{
	public class LoginModel : IModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public bool Admin { get; set; }
	}
}
