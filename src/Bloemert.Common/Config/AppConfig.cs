using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bloemert.Common.Config
{
  public class AppConfig : IAppConfig
  {
	 public string ApplicationName { get; }

	 public IConfiguration BaseConfiguration { get; }

	 public string ConnectionString { get; }

	 public AppConfig(string appName)
	 {
		// Get appsettings.json config info
		string envName = Environment.GetEnvironmentVariable("Einstein.WebAPI.EnvironmentName") ?? "Production";
		
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + appName;
		if ( !Directory.Exists(folderPath))
		{
		  Directory.CreateDirectory(folderPath);
		}

		string userEnvConfigFile = $"{folderPath}/appsettings.{envName}.json";

		// All config files must be added in THIS spot!
		BaseConfiguration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.AddJsonFile(userEnvConfigFile, optional: true)
			.AddJsonFile("logsettings.json", optional: true, reloadOnChange: true)
			.Build();

		this.ApplicationName = appName;
		this.ConnectionString = BaseConfiguration.GetConnectionString(this.GetValue("ConnectionStrings:DefaultDB"));
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
