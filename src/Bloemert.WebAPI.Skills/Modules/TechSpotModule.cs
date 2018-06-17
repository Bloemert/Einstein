using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Skills.Models;

namespace Bloemert.WebAPI.Skills.Modules
{
	public class TechSpotModule : BaseModule<TechSpotModule, ITechSpotRepository, TechSpot, TechSpotModel>
	{

		public TechSpotModule(IAppConfig appCfg, ITechSpotRepository techSpotRepository, 
			ITwoWayMapper<TechSpot, TechSpotModel> mapper)
			: base(appCfg, techSpotRepository, mapper, "/techspots")
		{

		}

	}
}
