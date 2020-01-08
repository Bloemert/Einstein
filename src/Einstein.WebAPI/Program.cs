using Autofac.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Einstein.WebAPI
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
				WebHost.CreateDefaultBuilder(args)
						//.UseKestrel()
						.ConfigureServices(services => services.AddAutofac())
						.UseContentRoot(Directory.GetCurrentDirectory())
						.UseIISIntegration()
						.UseSerilog()
						.UseStartup<Startup>();
	}
}
