using Autofac;
using Bloemert.Lib.Auto.Mapping.AutoMapper.Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Nancy.Owin;


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


		public void Configure(IApplicationBuilder app)
		{
			app.UseResponseBuffering();
			app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new Bootstrapper(IoCBuilder)));
		}
	}
}
