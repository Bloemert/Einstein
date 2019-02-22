using Bloemert.Data.Core;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Skills.Models;
using Serilog;

namespace Bloemert.WebAPI.Skills.Modules
{
	public class SectorModule : BaseModule<SectorModule, ISectorRepository, Sector, SectorModel>
	{

		public SectorModule(IAppConfig appCfg, ISectorRepository sectorRepository, 
			ITwoWayMapper<Sector, SectorModel> mapper,
			IUserIdentityProvider identityProvider,
			ILogger log)
			: base(appCfg, sectorRepository, mapper, identityProvider, log, "/skills/sectors")
		{

		}

	}
}
