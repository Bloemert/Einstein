using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Work.Entity;
using Bloemert.Lib.Common;
using Bloemert.Lib.Config;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bloemert.Data.Entity.Work.Repository.Implementation
{
	public class EmployeeRepository : BaseRepository<EmployeeRepository, Employee>, IEmployeeRepository
	{

		public override string TableName { get; set; } = @"Employees";


		public EmployeeRepository(ICommonRepositoryDependencies crd)
			: base(crd)
		{
		}
	}
}
