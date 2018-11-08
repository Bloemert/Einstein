using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloemert.Lib.Config
{
	public class AppConfig : IAppConfig
	{
		public static Lazy<AppConfig> Instance { get; } = new Lazy<AppConfig>(new AppConfig());

		public string ApplicationName { get; }

		public IConfiguration BaseConfiguration { get; }

		public string ConnectionString { get; }

		public AppConfig()
		{
			// Get appsettings.json config info
			string userEnvConfigFile = $"{ Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Einstein.WebAPI/appsettings.{ Environment.GetEnvironmentVariable("Einstein.WebAPI.EnvironmentName")}.json";

			// All config files must be added in THIS spot!
			BaseConfiguration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddJsonFile(userEnvConfigFile, optional: true)
				.AddJsonFile("logsettings.json", optional: true, reloadOnChange: true)
				.Build();

			this.ApplicationName = BaseConfiguration.GetValue<string>("AppConfig:Name");
			this.ConnectionString = BaseConfiguration.GetConnectionString(this.GetValue("ConnectionStrings:Default"));
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
