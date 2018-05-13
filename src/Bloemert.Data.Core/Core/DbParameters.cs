using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Core
{
	public class DbParameters
	{
		public static IDbParameters Create()
		{
			return new DefaultDbParameters();
		}

		public static IDbParameters Create(object dbParams)
		{
			return new DefaultDbParameters(dbParams);
		}


		public static IDbParameters Create(string name, object value)
		{
			return new DefaultDbParameters(name, value);
		}
	}
}
