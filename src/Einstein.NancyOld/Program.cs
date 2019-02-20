using System.IO;
using Microsoft.AspNetCore.Hosting;
using Einstein.WebAPI.Core;
using System;
using Bloemert.Lib.Config;
using System.Linq;
using Autofac;

namespace Einstein.WebAPI
{
	public class Program
	{
		public Program()
		{ 
			Startup.IoCBuilder = new ContainerBuilder();


			// Register IConfiguration
			Startup.IoCBuilder.RegisterInstance(AppConfig.Instance.Value)
				.As<IAppConfig>()
				.SingleInstance();


			// Start Nancy WebAPI using Kestrel as Webserver
			var host = new WebHostBuilder()
					.UseContentRoot(AppConfig.Instance.Value.GetValue("ContentRoot"))
					.UseWebRoot(AppConfig.Instance.Value.GetValue("WebRoot"))
					.UseKestrel()
					.UseStartup<Startup>()
					.UseUrls(AppConfig.Instance.Value.GetValues("URLs").Values.ToArray())
					.UseEnvironment(AppConfig.Instance.Value.GetValue("Environment"))
					.Build();

			host.Run();
		}

		static void Main(string[] args)
		{
			new Program();
		}
	}
}
