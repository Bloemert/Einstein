using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Skills.Models
{

	public class SkillCategoryModel : IModel
	{
		public int Id { get; set; }

		public bool Deleted { get; set; }



		public string Name { get; set; }

		public string Description { get; set; }
	}
}
