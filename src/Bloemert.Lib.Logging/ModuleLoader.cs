
using Autofac;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Bloemert.Lib.Logging
{
	public class ModuleLoader : Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			// Register referenced Modules
			builder.RegisterModule<Bloemert.Lib.Config.ModuleLoader>();

			// Get logsettings.json config info
			var configuration = new ConfigurationBuilder()
					.AddJsonFile("logsettings.json")
					.Build();

			// Set default Root logger
			Log.Logger = new LoggerConfiguration()
					.ReadFrom.Configuration(configuration)
#if DEBUG
					.WriteTo.Debug()
#endif
					.CreateLogger();

			// Register Logger by using AutofacSerilogInteration Extension
			builder.RegisterLogger();
		}

	}
}
