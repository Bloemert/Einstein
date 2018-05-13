using Bloemert.Lib.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Bloemert.Data.Core
{
	public class DbConnectionFactory : IDbConnectionFactory
	{
		public IAppConfig ApplicationConfig { get; }


		public DbConnectionFactory(IAppConfig appCfg)
		{
			ApplicationConfig = appCfg;
		}

		public IDbConnection Create(bool openConnection)
		{
			var connection = System.Data.SqlClient.SqlClientFactory.Instance.CreateConnection();
			connection.ConnectionString = ApplicationConfig.ConnectionString;

			if (openConnection)
			{
				connection.Open();
			}

			return connection;
		}
	}
}
