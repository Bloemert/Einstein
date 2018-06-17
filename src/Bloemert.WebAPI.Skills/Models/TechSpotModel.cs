using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Skills.Models
{

	public class TechSpotModel : IModel
	{
		public int SpotterId { get; set; }

		public DateTime SpottedOn { get; set; }


		public string Name { get; set; }

		public string Ring { get; set; }

		public string Quadrant { get; set; }

		public bool IsNew { get; set; }

		public string Description { get; set; }
	}
}
