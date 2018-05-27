using Autofac;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Nancy.Owin;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Core
{
	public class Startup
	{

		public static ContainerBuilder IoCBuilder { get; set; }

		public Startup(IHostingEnvironment env)
		{
			RegisterModules();
		}

		public void RegisterModules()
		{
			// Initialize DataLayer
			IoCBuilder.RegisterModule<Bloemert.Data.Core.ModuleLoader>();
			IoCBuilder.RegisterModule<Bloemert.Data.Entity.Auth.ModuleLoader>();
			IoCBuilder.RegisterModule<Bloemert.WebAPI.Auth.ModuleLoader>();
		}




		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				TelemetryConfiguration.Active.DisableTelemetry = true;
			}

			//app.UseResponseBuffering();
			app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new Bootstrapper(IoCBuilder)));
		}

	}
}
