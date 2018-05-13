using Bloemert.Data.Core.Tests.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Core.Tests.Repository
{
	public interface ITestRepository : IRepository<TestEntity>
	{
		bool InternalTests();
	}
}
