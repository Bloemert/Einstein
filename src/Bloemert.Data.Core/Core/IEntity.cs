
using System.Collections.Generic;
using System.Linq;

namespace Bloemert.Data.Core
{
	public interface IEntity 
	{
		int Id { get; set; }

		bool Deleted { get; set; }
	}
}
