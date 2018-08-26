using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Skills.Models
{

	public class SkillModel : IModel
	{
		public int Id { get; set; }

		public bool Deleted { get; set; }



		public string Name { get; set; }

		public string Description { get; set; }


		public string Version { get; set; }

		public int SkillTypeId { get; set; }

		public int SkillCategoryId { get; set; }
	}
}
