using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Skills.Models
{

	public class SpotModel : IModel
	{
		public int Id { get; set; }

		public bool Deleted { get; set; }


		public int SpotterId { get; set; }

		public DateTime SpottedOn { get; set; }


		public string Name { get; set; }

		public int RingId { get; set; }

		public int SectorId { get; set; }

		public bool IsNew { get; set; }

		public string Description { get; set; }
	}
}
