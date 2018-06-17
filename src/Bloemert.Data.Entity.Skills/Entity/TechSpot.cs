using Bloemert.Data.Core;
using System;

namespace Bloemert.Data.Entity.Skills.Entity
{
	public class TechSpot : BaseEntity
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
