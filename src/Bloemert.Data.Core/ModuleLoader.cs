
using Autofac;
using Autofac.Extras.AggregateService;
using Bloemert.Data.Core.Core;
using Bloemert.Lib.Config;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using System;
using System.Linq;
using System.Reflection;

namespace Bloemert.Data.Core
{
	public class ModuleLoader : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			// Register referenced Modules
			builder.RegisterModule<Bloemert.Lib.Config.ModuleLoader>();

			// Initialize and Register all neccesary base instances in proper order!
			builder.RegisterAggregateService<ICommonRepositoryDependencies>();

			builder.RegisterType<DbConnectionFactory>()
				.As<IDbConnectionFactory>()
				.SingleInstance();

			builder.RegisterType<DefaultDbExecutor>()
				.As<IDbExecutor>()
				.InstancePerDependency();

			builder.Register<ISessionFactory>((c, p) =>
			{
				IAppConfig appCfg = c.Resolve<IAppConfig>();

				return Fluently.Configure()
								 .Database(
										MsSqlConfiguration.MsSql2012.ConnectionString(appCfg.ConnectionString))
										.Mappings(AddAssemblies)
										.BuildSessionFactory();
			})
			.As<ISessionFactory>();


		}

		private static void AddAssemblies(MappingConfiguration fmc)
		{
			(from a in AppDomain.CurrentDomain.GetAssemblies()
			 select a
								 into assemblies
			 select assemblies)
								 .ToList()
								 .ForEach(a =>
								 {
									 //Maybe you need to inly include your NameSpace here.
									 if ((a.FullName.StartsWith("Bloemert") || a.FullName.StartsWith("Einstein")) && a.ExportedTypes.Any(et => et.IsAssignableTo<IEntity>() && !et.Name.Equals("BaseEntity")))
									 {
										 fmc.AutoMappings.Add(AutoMap.Assembly(a, new FNHAutomappingConfiguration())
																																		.Conventions.Add(
																																				Table.Is(x => x.EntityType.Name + "s"),
																																				DynamicInsert.AlwaysTrue(),
																																				DynamicUpdate.AlwaysTrue(),
																																				PrimaryKey.Name.Is(x => "Id"),
																																				DefaultLazy.Always(),
																																				ForeignKey.EndsWith("Id")
																																			).UseOverridesFromAssembly(a));

									 }
								 }
			 );
		}

	}
}
