
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloemert.Data.Core
{
	public interface IEntity 
	{
		int Id { get; set; }


		DateTime EffectiveStartedOn { get; set; }
		int EffectiveStartedBy { get; set; }

		DateTime EffectiveModifiedOn { get; set; }
		int EffectiveModifiedBy { get; set; }

		DateTime EffectiveEndedOn { get; set; }
		int EffectiveEndedBy { get; set; }

	}
}
