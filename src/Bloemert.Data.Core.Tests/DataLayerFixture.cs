using Autofac;
using Bloemert.Data.Core.Tests.Repository;
using Bloemert.Data.Core.Tests.Repository.Implementation;
using System;
using Xunit;

namespace Bloemert.Data.Core.Tests
{
	public class DataLayerFixture : IDisposable
	{
		public IContainer IoC { get; }

		public IDbExecutor Db { get; }


		public DataLayerFixture()
		{
			ContainerBuilder builder = new ContainerBuilder();

			builder.RegisterModule<Bloemert.Data.Core.ModuleLoader>();
			builder.RegisterModule<Bloemert.Lib.Config.ModuleLoader>();

			builder.RegisterType<TestRepository>()
							.As<ITestRepository>()
							.SingleInstance();


			IoC = builder.Build();


			Db = IoC.Resolve<IDbExecutor>();

			CreateTestTable();
		}


		private void CreateTestTable()
		{
			Db.Execute(@"
				IF (EXISTS (SELECT * 
												 FROM INFORMATION_SCHEMA.TABLES 
												 WHERE TABLE_SCHEMA = 'dbo' 
												 AND  TABLE_NAME = 'Tests'))
				BEGIN
					drop table dbo.Tests;
				END

				CREATE TABLE [dbo].[Tests]
				(
					[Id] INT NOT NULL IDENTITY, 
					[Deleted] BIT NOT NULL DEFAULT 0, 
					[Name] NVARCHAR(255) NULL,
					[Number] INTEGER NULL
					CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
					(
						[Id] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
				);
			");
		}




		private void DropTestTable()
		{
			Db.Execute(@"
				DROP TABLE Tests;
			", null);
		}

		public void Dispose()
		{
			// ... clean up test data from the database ...
			DropTestTable();
		}

	}
	[CollectionDefinition("Data Layer Collection")]
	public class DatabaseCollection : ICollectionFixture<DataLayerFixture>
	{
		// This class has no code, and is never created. Its purpose is simply
		// to be the place to apply [CollectionDefinition] and all the
		// ICollectionFixture<> interfaces.
	}
}
