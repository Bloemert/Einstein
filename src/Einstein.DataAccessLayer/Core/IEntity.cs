using System;

namespace Einstein.DataAccessLayer.Core
{
	public interface IEntity
	{
		Guid Id { get; }

		DateTime EffectiveStartedOn { get; set; }
		Guid EffectiveStartedBy { get; set; }

		DateTime EffectiveModifiedOn { get; set; }
		Guid EffectiveModifiedBy { get; set; }

		DateTime EffectiveEndedOn { get; set; }
		Guid EffectiveEndedBy { get; set; }

	}
}
