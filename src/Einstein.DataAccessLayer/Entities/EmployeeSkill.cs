
using Einstein.DataAccessLayer.Core;

namespace Einstein.DataAccessLayer.Entities
{
	public class EmployeeSkill : BaseEntity
	{
		public virtual int EmployeeId { get; set; }

		public virtual int SkillId { get; set; }

		public virtual int ScoreId { get; set; }

	}
}
