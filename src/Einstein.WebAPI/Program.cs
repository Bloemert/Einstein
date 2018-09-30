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
			Console.WriteLine("Starting Einstein.RestAPI...");

			// Get appsettings.json config info
			if (!File.Exists("appsettings.json"))
			{
				Console.WriteLine("[Error] No appsettings.json configfile found!");
				return;
			}

			string envName = Environment.GetEnvironmentVariable("Einstein.WebAPI.EnvironmentName");
			string userEnvConfigFile = null;
			if (string.IsNullOrEmpty(envName))
			{
				Console.WriteLine("[Info] Environment variable 'Einstein.WebAPI.EnvironmentName' is not set so only using appsettings.json in current directory!");
			}
			else
			{
				userEnvConfigFile = $"{ Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Einstein.WebAPI/appsettings.{ envName }.json";
			}

			if ( !String.IsNullOrEmpty(userEnvConfigFile) && File.Exists(userEnvConfigFile) )
			{
				Console.WriteLine($"[Info] Extra settings loading from '{userEnvConfigFile}'!");
				Config = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.AddJsonFile(userEnvConfigFile, optional: true)
					.Build();
			}
			else
			{
				Console.WriteLine("[Warning] No (optional) configfile found at " + userEnvConfigFile);
				Config = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.Build();
			}

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
					.UseKestrel()
					.UseContentRoot(AppConfig.GetValue("ContentRoot"))
					//.UseWebRoot(AppConfig.GetValue("WebRoot"))
					.UseUrls(AppConfig.GetValues("URLs").Values.ToArray())
					.UseEnvironment(AppConfig.GetValue("Environment"))
					.UseStartup<Startup>()
					.Build();

			host.Run();
		}

		static void Main(string[] args)
		{
			new Program();
		}
	}
}
