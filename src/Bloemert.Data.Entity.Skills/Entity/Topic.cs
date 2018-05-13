using System.Collections.Generic;

namespace Einstein.Data.Entity.Knowledge
{
	public class Topic : Item
	{
		// Association(s)
		public virtual Area Area { get; set; }

		public virtual IList<Score> Score { get; set; }
	}
}
