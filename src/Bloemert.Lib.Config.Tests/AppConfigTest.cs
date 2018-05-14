using Autofac;
using System;
using Xunit;

namespace Bloemert.Lib.Config.Tests
{
	public class AppConfigTest
	{
		IContainer Container { get; }

		public AppConfigTest()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<Bloemert.Lib.Config.ModuleLoader>();

			Container = builder.Build();
		}

		[Fact]
		public void TestAppConfig()
		{
			IAppConfig appCfg = Container.Resolve<IAppConfig>();

			Assert.Equal("DummyTest!", appCfg.ConnectionString);
		}
	}
}
