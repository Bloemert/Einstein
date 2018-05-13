using System.Collections.Generic;

namespace Einstein.Data.Entity.Knowledge
{
	public class Area : Item
	{
		// Association(s)
		public virtual IList<Topic> Topics { get; set; }
	}
}
