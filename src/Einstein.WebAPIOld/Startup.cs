using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bloemert.Data.Core;
using Einstein.WebAPI.Auth;
using Einstein.WebAPI.GraphQL;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using System;
using System.Reflection;

namespace Einstein.WebAPI
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
													.SetBasePath(env.ContentRootPath)
													.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
													.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
													.AddEnvironmentVariables();
			this.Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		public ILifetimeScope IoC { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddMvc().AddControllersAsServices();

			var builder = new ContainerBuilder();

			// When you do service population, it will include your controller
			// types automatically.
			builder.Populate(services);

			// If you want to set up a controller for, say, property injection
			// you can override the controller registration after populating services.
			//builder.RegisterType<MyController>().PropertiesAutowired();



			ConfigureContainer(builder);

			this.IoC = builder.Build();
			return new AutofacServiceProvider(this.IoC);
		}


		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule<Bloemert.Lib.Logging.ModuleLoader>();
			builder.RegisterModule<Bloemert.Lib.Config.ModuleLoader>();

			builder.RegisterModule<Bloemert.Data.Core.ModuleLoader>();
			builder.RegisterModule<Bloemert.Data.Entity.Auth.ModuleLoader>();
			builder.RegisterModule<Bloemert.Data.Entity.Work.ModuleLoader>();


			builder.RegisterType<DefaultUserIdentityProvider>()
			.As<IUserIdentityProvider>()
			.InstancePerLifetimeScope();

			builder.RegisterInstance(SchemaBuilder.FromObject<DataContext>())
				.SingleInstance();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
