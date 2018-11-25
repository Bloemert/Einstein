using Bloemert.Data.Core;
using System;

namespace Bloemert.Data.Entity.Skills.Entity
{
	public class Sector : BaseEntity
	{
		public virtual int Seqno { get; set; }

		public virtual string Name { get; set; }

		public virtual string Description { get; set; }

	}
}
