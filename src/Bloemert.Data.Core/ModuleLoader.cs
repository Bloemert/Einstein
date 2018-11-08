
using Autofac;
using Autofac.Extras.AggregateService;

namespace Bloemert.Data.Core
{
	public class ModuleLoader : Module
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

		}

	}
}
