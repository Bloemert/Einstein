using System.IO;
using Microsoft.AspNetCore.Hosting;
using Einstein.WebAPI.Core;
using Microsoft.Extensions.Configuration;
using System;
using Bloemert.Lib.Config;
using System.Linq;
using Autofac;

namespace Einstein.WebAPI
{
	public class Program
	{

		public static IConfiguration Config { get; set; }

		public static AppConfig			 AppConfig { get; set; }


		public Program()
		{
			// Get appsettings.json config info
			string userEnvConfigFile = $"{ Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Einstein.WebAPI/appsettings.{ Environment.GetEnvironmentVariable("Einstein.WebAPI.EnvironmentName")}.json";

			Config =  new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddJsonFile(userEnvConfigFile, optional: true)
				.Build();

			AppConfig = new AppConfig(Config);

			Startup.IoCBuilder = new ContainerBuilder();


			// Register IConfiguration
			Startup.IoCBuilder.RegisterInstance(Config)
				.As<IConfiguration>()
				.SingleInstance();

			Startup.IoCBuilder.RegisterInstance(AppConfig)
				.As<IAppConfig>()
				.SingleInstance();


			// Start Nancy WebAPI using Kestrel as Webserver
			var host = new WebHostBuilder()
					.UseContentRoot(AppConfig.GetValue("ContentRoot"))
					.UseWebRoot(AppConfig.GetValue("WebRoot"))
					.UseKestrel()
					.UseStartup<Startup>()
					.UseUrls(AppConfig.GetValues("URLs").Values.ToArray())
					.Build();

			host.Run();
		}

		static void Main(string[] args)
		{
			new Program();
		}
	}
}
