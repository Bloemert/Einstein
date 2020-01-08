using Einstein.DataAccessLayer.Core;
using System;

namespace Einstein.DataAccessLayer.Entities
{
  public class Spot : BaseEntity
	{
		public virtual int SpotterId { get; set; }

		public virtual DateTime SpottedOn { get; set; }


		public virtual string Name { get; set; }

		public virtual int RingId { get; set; }

		public virtual int SectorId { get; set; }

		public virtual bool IsNew { get; set; }

		public virtual string Description { get; set; }

	}
}
