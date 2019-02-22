using Autofac;
using Bloemert.Data.Core.Tests.Entity;
using NHibernate;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Core.Tests.Repository.Implementation
{
	public class TestRepository : BaseRepository<TestRepository, TestEntity>, ITestRepository
	{

		public override string TableName { get; set; } = @"Tests";


		public TestRepository(ILifetimeScope ioc,
									ISessionFactory sessionFactory,
									ILogger log)
			: base(ioc, sessionFactory, log)
		{

		}


		public bool InternalTests()
		{
			bool result = false;



			return result;
		}

	}
}
