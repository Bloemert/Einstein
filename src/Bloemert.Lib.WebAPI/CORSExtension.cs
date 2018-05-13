using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloemert.Lib.WebAPI
{
	public static class CORSExtension
	{
		public static void EnableCORS(this Nancy.Bootstrapper.IPipelines pipelines)
		{
			pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
			{
				if (ctx.Request.Headers.Keys.Contains("Origin"))
				{
					var origins = "" + string.Join(" ", ctx.Request.Headers["Origin"]);
					ctx.Response.Headers["Access-Control-Allow-Origin"] = origins;
					ctx.Response.Headers["Access-Control-Allow-Credentials"] = "true";

					if (ctx.Request.Method == "OPTIONS")
					{
						// handle CORS preflight request
						ctx.Response.Headers["Access-Control-Allow-Methods"] =
								"GET, POST, PUT, DELETE, OPTIONS";

						if (ctx.Request.Headers.Keys.Contains("Access-Control-Request-Headers"))
						{
							var allowedHeaders = "" + string.Join(
									", ", ctx.Request.Headers["Access-Control-Request-Headers"]);
							ctx.Response.Headers["Access-Control-Allow-Headers"] = allowedHeaders;
						}
					}
				}
			});
		}
	}
}
