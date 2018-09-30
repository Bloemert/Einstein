using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.WebAPI.Models
{
	public class InfoSection
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public string Link { get; set; }

		public IList<InfoItem> Items { get; set; }
	}
}
