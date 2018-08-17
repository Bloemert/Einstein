using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Skills.Models;

namespace Bloemert.WebAPI.Skills.Modules
{
	public class SectorModule : BaseModule<SectorModule, ISectorRepository, Sector, SectorModel>
	{

		public SectorModule(IAppConfig appCfg, ISectorRepository spotRepository, 
			ITwoWayMapper<Sector, SectorModel> mapper)
			: base(appCfg, spotRepository, mapper, "/sectors")
		{

		}

	}
}
