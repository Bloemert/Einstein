using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Core.Core
{
	public class BaseMap<T> : ClassMap<T> where T : IEntity
	{
		public BaseMap()
		{
			Id(x => x.Id);
			Map(x => x.EffectiveStartedOn);
			Map(x => x.EffectiveStartedBy);

			Map(x => x.EffectiveModifiedOn);
			Map(x => x.EffectiveModifiedBy);

			Map(x => x.EffectiveEndedOn);
			Map(x => x.EffectiveEndedBy);
		}
	}

}
