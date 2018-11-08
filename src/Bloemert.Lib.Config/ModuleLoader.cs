
using Autofac;
using Bloemert.Lib.Config.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace Bloemert.Lib.Config
{
	public class ModuleLoader : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//DEBUG Alert: When Serilog or one of its Sinks has problems use the Serilog SelfLog...
			//var file = File.CreateText(@"c:\temp\Logs\SerilogSelf.log.txt");
			//Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));

			// HACK Alert: The MSSQLServer sink does not (yet) call GetConnectionString for Named Connections, so we have to do this...
			string logDB = AppConfig.Instance.Value.BaseConfiguration.GetValue<string>("Serilog:WriteTo:0:Args:connectionString") ?? "DefaultDB";

			if (!logDB.Contains("="))
			{
				AppConfig.Instance.Value.BaseConfiguration["Serilog:WriteTo:0:Args:connectionString"] = AppConfig.Instance.Value.BaseConfiguration.GetConnectionString(logDB);
			}

			// Set default Root logger
			var logConfig = new LoggerConfiguration()
					.ReadFrom.Configuration(AppConfig.Instance.Value.BaseConfiguration)
#if DEBUG
					.WriteTo.Debug()
#endif
			;
			Log.Logger = logConfig.CreateLogger();


			// Register Configuration
			builder.RegisterInstance<AppConfig>(AppConfig.Instance.Value)
				.As<IAppConfig>();
		}

	}
}
