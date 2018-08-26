using Bloemert.Data.Core;
using Bloemert.Lib.WebAPI;
using Newtonsoft.Json;
using System;

namespace Bloemert.WebAPI.Work.Models
{
	public class EmployeeSkillModel : IModel
	{
		public int Id { get; set; }

		public bool Deleted { get; set; }


		public int EmployeeId { get; set; }

		public int SkillId { get; set; }

		public int ScoreId { get; set; }

	}
}
