using Bloemert.Data.Core;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Data.Entity.Skills.Repository;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Bloemert.Lib.WebAPI;
using Bloemert.WebAPI.Skills.Models;

namespace Bloemert.WebAPI.Skills.Modules
{
	public class RingModule : BaseModule<RingModule, IRingRepository, Ring, RingModel>
	{

		public RingModule(IAppConfig appCfg, IRingRepository ringRepository, 
			ITwoWayMapper<Ring, RingModel> mapper,
			IUserIdentityProvider identityProvider)
			: base(appCfg, ringRepository, mapper, identityProvider, "/skills/rings")
		{

		}

	}
}
