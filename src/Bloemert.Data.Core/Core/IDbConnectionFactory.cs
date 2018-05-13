using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Bloemert.Data.Core
{
	public interface IDbConnectionFactory
	{
		IDbConnection Create(bool openConnection = true);
	}
}
