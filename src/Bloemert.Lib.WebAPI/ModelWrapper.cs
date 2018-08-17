using Bloemert.Data.Entity.Auth;
using Bloemert.Data.Entity.Auth.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Lib.WebAPI
{
	[Serializable]
	public class ModelWrapper<DT>
	{
		public DT Data { get; set; }

		public string Error { get; set; }

		public User ActiveUser { get; set; }

		public string tag { get; set; }
	}
}
