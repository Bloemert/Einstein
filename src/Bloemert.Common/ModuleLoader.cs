
using Autofac;
using AutofacSerilogIntegration;
using Bloemert.Common.Config;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System.IO;

namespace Bloemert.Common
{
  public class ModuleLoader : Autofac.Module
  {

	public string ApplicationName { get; set; }


	 protected override void Load(ContainerBuilder builder)
	 {
		// Create and Register Configuration
		var appConfig = new AppConfig(ApplicationName);

		builder.RegisterInstance(appConfig)
			.As<IAppConfig>();


		/// DEBUG Alert: When Serilog or one of its Sinks has problems use the Serilog SelfLog...
		//var file = File.CreateText(@"c:\temp\SerilogSelf.log.txt");
		//Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));

		// Set default Root logger
		var logConfig = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.ReadFrom.Configuration(appConfig.BaseConfiguration)
			;
		Log.Logger = logConfig.CreateLogger();

		Log.Logger.Information("Logging system initiated.");

		// Register Logger by using AutofacSerilogInteration Extension
		builder.RegisterLogger();
	 }

  }
}
