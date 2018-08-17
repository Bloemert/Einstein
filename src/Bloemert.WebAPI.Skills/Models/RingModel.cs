using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Skills.Models
{

	public class RingModel : IModel
	{
		public int Id { get; set; }

		public bool Deleted { get; set; }


		public int Seqno { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
