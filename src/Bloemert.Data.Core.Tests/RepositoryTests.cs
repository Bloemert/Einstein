using Autofac;
using Bloemert.Data.Core.Tests.Entity;
using Bloemert.Data.Core.Tests.Repository;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bloemert.Data.Core.Tests
{

	[Collection("Data Layer Collection")]
	public class RepositoryTests
	{
		DataLayerFixture fixture;

		private const string Skip = "Needs Database: Check appsettings.json of this UnitTest project! Not running now!";

		public RepositoryTests(DataLayerFixture fxtr)
		{
			fixture = fxtr;
		}

		[Fact(Skip = Skip)]
		public void TestNewEntityAndSave()
		{
			// Prepare
			ITestRepository testRepository = fixture.IoC.Resolve<ITestRepository>();

			TestEntity tester = testRepository.NewEntity();
			tester.Name = "Henry Roeland";
			tester.Number = 1977;
			TestEntity testerSaved = testRepository.SaveEntity(tester);

			// Test
			Assert.True(testerSaved.Id != null);
			//Assert.False(testerSaved.Deleted);
			Assert.Equal(tester.Name, testerSaved.Name);
			Assert.Equal(tester.Number, testerSaved.Number);

			// Cleanup
			Assert.True(testRepository.DeleteEntity(testerSaved.Id));
		}

		[Fact(Skip = Skip)]
		public async Task TestNewEntityAndSaveAsync()
		{
			// Prepare
			ITestRepository testRepository = fixture.IoC.Resolve<ITestRepository>();

			TestEntity tester = testRepository.NewEntity();
			tester.Name = "Henry Roeland";
			tester.Number = 1977;
			TestEntity testerSaved = await testRepository.SaveEntityAsync(tester);

			// Test
			//Assert.True(testerSaved.Id > 0);
			//Assert.False(testerSaved.Deleted);
			Assert.Equal(tester.Name, testerSaved.Name);
			Assert.Equal(tester.Number, testerSaved.Number);

			// Cleanup
			Assert.True(await testRepository.DeleteEntityAsync(testerSaved.Id));
		}



		[Fact(Skip = Skip)]
		public void TestGetEntity()
		{
			// Prepare
			ITestRepository testRepository = fixture.IoC.Resolve<ITestRepository>();

			TestEntity tester = testRepository.NewEntity();
			tester.Name = "Henry Roeland";
			tester.Number = 1977;
			TestEntity testerSaved = testRepository.SaveEntity(tester);

			// Test
			TestEntity getTestEntity = testRepository.GetEntity(testerSaved.Id);
			//Assert.True(getTestEntity.Id > 0);
			//Assert.False(getTestEntity.Deleted);
			Assert.Equal(tester.Name, getTestEntity.Name);
			Assert.Equal(tester.Number, getTestEntity.Number);

			// Cleanup
			Assert.True(testRepository.DeleteEntity(testerSaved.Id));
		}

		[Fact(Skip = Skip)]
		public async Task TestGetEntityAsync()
		{
			// Prepare
			ITestRepository testRepository = fixture.IoC.Resolve<ITestRepository>();

			TestEntity tester = testRepository.NewEntity();
			tester.Name = "Henry Roeland";
			tester.Number = 1977;
			TestEntity testerSaved = await testRepository.SaveEntityAsync(tester);

			// Test
			TestEntity getTestEntity = testRepository.GetEntity(testerSaved.Id);
			//Assert.True(getTestEntity.Id > 0);
			//Assert.False(getTestEntity.Deleted);
			Assert.Equal(tester.Name, getTestEntity.Name);
			Assert.Equal(tester.Number, getTestEntity.Number);

			// Cleanup
			Assert.True(await testRepository.DeleteEntityAsync(testerSaved.Id));
		}



		[Fact(Skip = Skip)]
		public void TestListEntity()
		{
			// Prepare
			ITestRepository testRepository = fixture.IoC.Resolve<ITestRepository>();

			TestEntity tester1 = testRepository.NewEntity();
			tester1.Name = "Henry Roeland";
			tester1.Number = 21061977;
			TestEntity tester1Saved = testRepository.SaveEntity(tester1);

			TestEntity tester2 = testRepository.NewEntity();
			tester2.Name = "Klaas Baarssen";
			tester2.Number = 0;
			TestEntity tester2Saved = testRepository.SaveEntity(tester2);

			TestEntity tester3 = testRepository.NewEntity();
			tester3.Name = "Gerjan Konterman";
			tester3.Number = 0;
			TestEntity tester3Saved = testRepository.SaveEntity(tester3);


			// Test
			IList<TestEntity> tests = testRepository.ListQuery();
			Assert.True(3 <= tests.Count);

			Assert.Equal(tester1Saved.Id, tests.Where(x => x.Name.Equals(tester1.Name)).FirstOrDefault().Id);
			Assert.Equal(tester2Saved.Id, tests.Where(x => x.Name.Equals(tester2.Name)).FirstOrDefault().Id);
			Assert.Equal(tester3Saved.Id, tests.Where(x => x.Name.Equals(tester3.Name)).FirstOrDefault().Id);


			// Cleanup
			Assert.True(testRepository.DeleteEntity(tester1Saved.Id));
			Assert.True(testRepository.DeleteEntity(tester2Saved.Id));
			Assert.True(testRepository.DeleteEntity(tester3Saved.Id));
		}


		[Fact(Skip = Skip)]
		public async Task TestListEntityAsync()
		{
			// Prepare
			ITestRepository testRepository = fixture.IoC.Resolve<ITestRepository>();

			TestEntity tester1 = testRepository.NewEntity();
			tester1.Name = "Henry Roeland";
			tester1.Number = 21061977;
			TestEntity tester1Saved = await testRepository.SaveEntityAsync(tester1);

			TestEntity tester2 = testRepository.NewEntity();
			tester2.Name = "Klaas Baarssen";
			tester2.Number = 0;
			TestEntity tester2Saved = await testRepository.SaveEntityAsync(tester2);

			TestEntity tester3 = testRepository.NewEntity();
			tester3.Name = "Gerjan Konterman";
			tester3.Number = 0;
			TestEntity tester3Saved = await testRepository.SaveEntityAsync(tester3);


			// Test
			IList<TestEntity> tests = await testRepository.ListQueryAsync();
			Assert.Equal(3, tests.Count);

			Assert.Equal(tester1Saved.Id, tests.Where(x => x.Name.Equals(tester1.Name)).FirstOrDefault().Id);
			Assert.Equal(tester2Saved.Id, tests.Where(x => x.Name.Equals(tester2.Name)).FirstOrDefault().Id);
			Assert.Equal(tester3Saved.Id, tests.Where(x => x.Name.Equals(tester3.Name)).FirstOrDefault().Id);


			// Cleanup
			Assert.True(await testRepository.DeleteEntityAsync(tester1Saved.Id));
			Assert.True(await testRepository.DeleteEntityAsync(tester2Saved.Id));
			Assert.True(await testRepository.DeleteEntityAsync(tester3Saved.Id));
		}

	}
}
