using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.WebAPI.Models
{
	public class Info
	{
		public string Title { get; set; }

		public InfoSection NancySection { get; set; }

		public InfoSection DotNetSection { get; set; }

		public InfoSection AssembliesSection { get; set; }
	}
}
