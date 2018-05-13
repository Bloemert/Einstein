using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloemert.Lib.Config
{
	public class AppConfig : IAppConfig
	{
		public string ApplicationName { get; }

		public IConfiguration BaseConfiguration { get; }

		public string ConnectionString { get; }

		public AppConfig(IConfiguration baseCfg)
		{
			this.BaseConfiguration = baseCfg;
			this.ApplicationName = baseCfg.GetValue<string>("AppConfig:Name");
			this.ConnectionString = this.GetValue("ConnectionStrings:Default:Value");
		}


		public string GetValue(string key)
		{
			return BaseConfiguration.GetValue<string>(String.Format("Configurations:{0}:{1}", ApplicationName, key));
		}

		public IDictionary<string, string> GetValues(string key)
		{
			IConfigurationSection section = BaseConfiguration.GetSection(String.Format("Configurations:{0}:{1}", ApplicationName, key));
			return section.AsEnumerable().ToDictionary(x => x.Key, x => x.Value);
		}
	}
}
