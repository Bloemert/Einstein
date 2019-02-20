using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bloemert.Lib.WebAPI
{
	public class CustomJsonSerializer : JsonSerializer
	{
		public CustomJsonSerializer()
		{
			this.ContractResolver = new CamelCasePropertyNamesContractResolver();
			this.Formatting = Formatting.Indented;
		}
	}

}
