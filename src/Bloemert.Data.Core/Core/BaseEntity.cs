
using System.Collections.Generic;
using System.Linq;

namespace Bloemert.Data.Core
{
	public class BaseEntity : IEntity
	{
		public int Id { get; set; }

		public bool Deleted { get; set; }

	}
}
