using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace Bloemert.Lib.WebAPI.Json
{
	public class JsonNetModelBinder : IModelBinder
	{
		private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			Formatting = Formatting.Indented
		};

		public object Bind(NancyContext ctx, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
		{
			JsonSerializer serializer = new CustomJsonSerializer();

			StreamReader sr = new StreamReader(ctx.Request.Body);

			return JsonConvert.DeserializeObject(sr.ReadToEnd(), modelType, Settings);
		}

		public bool CanBind(Type modelType)
		{
				return true;
		}

	}
}
