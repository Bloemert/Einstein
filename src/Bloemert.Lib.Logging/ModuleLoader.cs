
using Autofac;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace Bloemert.Lib.Logging
{
	public class ModuleLoader : Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			// Register Logger by using AutofacSerilogInteration Extension
			// Note: Logger.Log is set in ModuleLoader of Bloemert.Lib.Config because of circular reference(s).
			builder.RegisterLogger();
		}

	}
}
