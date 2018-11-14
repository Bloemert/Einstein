using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Skills.Models
{

	public class SkillCategoryModel : IModel
	{
		public int Id { get; set; }

		public DateTime EffectiveStartedOn { get; set; }
		public int EffectiveStartedBy { get; set; }

		public DateTime EffectiveModifiedOn { get; set; }
		public int EffectiveModifiedBy { get; set; }

		public DateTime EffectiveEndedOn { get; set; }
		public int EffectiveEndedBy { get; set; }




		public string Name { get; set; }

		public string Description { get; set; }
	}
}
