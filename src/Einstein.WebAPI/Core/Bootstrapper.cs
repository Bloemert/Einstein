using Autofac;
using Autofac.Core;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.Lib.WebAPI.Json;
using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Einstein.WebAPI.Core
{
	public class Bootstrapper : AutofacNancyBootstrapper
	{

		private ContainerBuilder IoCBuilder { get; }


		public Bootstrapper(ContainerBuilder iocBuilder)
		{
			IoCBuilder = iocBuilder;
		}



		protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);

			pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
																								container.Resolve<IUserValidator>(),
																									"Einstein",
																									UserPromptBehaviour.Never));

			IAppConfig appConfig = container.Resolve<IAppConfig>();
			pipelines.EnableCORS(appConfig);
		}



		public override void Configure(INancyEnvironment environment)
		{
			base.Configure(environment);

			environment.StaticContent(@"d:\temp");
		}

		protected override void ConfigureApplicationContainer(ILifetimeScope container)
		{
			// Perform registration that should have an application lifetime
		}
		/*
		protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
		{
		// Perform registrations that should have a request lifetime

		}
		*/

		protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
		{
			// No registrations should be performed in here, however you may
			// resolve things that are needed during request startup.
		}


		/// <summary>
		/// Bind the given module types into the container
		/// </summary>
		/// <param name="container">Container to register into</param>
		/// <param name="moduleRegistrationTypes"><see cref="INancyModule"/> types</param>
		protected override void RegisterRequestContainerModules(ILifetimeScope container, IEnumerable<ModuleRegistration> moduleRegistrationTypes)
		{
			container.Update(builder =>
			{
				/*
				foreach (var moduleRegistrationType in moduleRegistrationTypes)
				{
					if (!moduleRegistrationType.ModuleType.Name.StartsWith("BaseModule"))
					{
						builder.RegisterType(moduleRegistrationType.ModuleType)
							.As<INancyModule>()
							.InstancePerRequest();
					}
				}*/
				// Initialize and Register all neccesary base instances in proper order!
				builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				 .Where(t => !t.Name.StartsWith("BaseModule") && t.IsAssignableTo<INancyModule>())
				 .AsImplementedInterfaces()
				 .InstancePerRequest();
			});
		}


		protected override ILifetimeScope GetApplicationContainer()
		{
			IoCBuilder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>();
			IoCBuilder.RegisterType<JsonNetModelBinder>()
									.WithParameter(
											new ResolvedParameter(
													 (pi, ctx) => pi.ParameterType == typeof(JsonSerializer),
													 (pi, ctx) => ctx.Resolve<JsonSerializer>()))
									.As<IModelBinder>();

			// Return application container instance
			return IoCBuilder.Build();
		}

	}
}
