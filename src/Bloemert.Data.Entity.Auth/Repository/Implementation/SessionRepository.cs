using Bloemert.Data.Core;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Lib.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Bloemert.Data.Entity.Auth.Repository.Implementation
{
	public class SessionRepository : BaseRepository<SessionRepository, Session>, ISessionRepository
	{

		public override string TableName { get; set; } = @"Sessions";


		public SessionRepository(ICommonRepositoryDependencies crd)
			: base(crd)
		{

		}

	}
}
