
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
			 * This only exists for failback and UnitTests that need generic AppConfig
			 */
			// Get appsettings.json config info
			var configuration = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.Build();


			// Register IConfiguration
			builder.RegisterInstance(configuration)
				.As<IConfiguration>()
				.IfNotRegistered(typeof(IConfiguration))
				.SingleInstance();

			builder.RegisterType<AppConfig>()
				.As<IAppConfig>()
				.IfNotRegistered(typeof(IAppConfig))
				.SingleInstance();
		}

	}
}
