
using Bloemert.Data.Core;

namespace Bloemert.Data.Entity.Admin.Entity
{
	public class Setting : BaseEntity
	{
		public virtual string Description { get; set; }
		public virtual string Key { get; set; }
		public virtual string Name { get; set; }
		public virtual string Remarks { get; set; }
		public virtual string Value { get; set; }
	}
}
