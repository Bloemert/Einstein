using Autofac;
using Bloemert.Data.Core;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Common;
using Bloemert.Lib.Config;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bloemert.Data.Entity.Skills.Repository.Implementation
{
	public class TechSpotRepository : BaseRepository<TechSpotRepository, TechSpot>, ITechSpotRepository
	{

		public override string TableName { get; set; } = @"TechSpots";

		public override Regex ExcludePropertyMatch { get; set; }

		public TechSpotRepository(ICommonRepositoryDependencies crd)
			: base(crd)
		{
		}

	}
}
