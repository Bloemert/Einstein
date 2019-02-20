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
	public class SpotModule : BaseModule<SpotModule, ISpotRepository, Spot, SpotModel>
	{

		public SpotModule(IAppConfig appCfg, ISpotRepository spotRepository, 
			ITwoWayMapper<Spot, SpotModel> mapper,
			IUserIdentityProvider identityProvider,
			ILogger log)
			: base(appCfg, spotRepository, mapper, identityProvider, log, "/skills/spots")
		{

		}

	}
}
