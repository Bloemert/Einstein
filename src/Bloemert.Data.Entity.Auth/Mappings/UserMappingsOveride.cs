using Bloemert.Data.Entity.Auth.Entity;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Entity.Auth.Mappings
{
	public class UserMappingOverride : IAutoMappingOverride<User>
	{
		public void Override(AutoMapping<User> mapping)
		{
			//mapping.DynamicUpdate();
		}
	}
}
