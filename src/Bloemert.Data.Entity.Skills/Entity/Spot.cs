using Bloemert.Data.Core;
using System;

namespace Bloemert.Data.Entity.Skills.Entity
{
	public class Spot : BaseEntity
	{
		public int SpotterId { get; set; }

		public DateTime SpottedOn { get; set; }


		public string Name { get; set; }

		public int RingId { get; set; }

		public int SectorId { get; set; }

		public bool IsNew { get; set; }

		public string Description { get; set; }

	}
}
