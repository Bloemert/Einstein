
using Bloemert.Data.Core.Core;
using System;

namespace Bloemert.Data.Core
{
	public abstract class BaseEntity : IEntity
	{
		public virtual Guid Id { get; set; }

		public virtual DateTime EffectiveStartedOn { get; set; }
		public virtual Guid EffectiveStartedBy { get; set; }

		public virtual DateTime EffectiveModifiedOn { get; set; }
		public virtual Guid EffectiveModifiedBy { get; set; }

		public virtual DateTime EffectiveEndedOn { get; set; }
		public virtual Guid EffectiveEndedBy { get; set; }

	}
}
