using Autofac;
using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloemert.Data.Core.Core
{
	public class FNHAutomappingConfiguration : DefaultAutomappingConfiguration
	{
		public override bool ShouldMap(Type type)
		{
			return type.IsAssignableTo<IEntity>() && !type.Name.Equals("BaseEntity");
		}
	}
}
