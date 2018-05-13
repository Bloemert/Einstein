
using Autofac;
using Bloemert.Lib.Config.Extensions;
using Microsoft.Extensions.Configuration;
using System;

namespace Bloemert.Lib.Config
{
	public class ModuleLoader : Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			/* Moved to the front line in Program.cs
			// Get appsettings.json config info
			string userEnvConfigFile = $"{ Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Einstein.WebAPI/appsettings.{ Environment.GetEnvironmentVariable("Einstein.WebAPI.EnvironmentName")}.json";

			var configuration = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.AddJsonFile(userEnvConfigFile, optional: true)
					.Build();

			// Register IConfiguration
			builder.RegisterInstance(configuration)
				.As<IConfiguration>()
				.SingleInstance();

			builder.RegisterType<AppConfig>()
				.As<IAppConfig>()
				.SingleInstance();
				*/
		}

	}
}
