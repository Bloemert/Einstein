using Bloemert.Lib.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloemert.Lib.WebAPI
{
	public static class CORSExtension
	{
		public static void EnableCORS(this Nancy.Bootstrapper.IPipelines pipelines,  IAppConfig appConfig)
		{
			string sCORSAllowedOriginBase = appConfig.GetValue("CORSAllowedOriginBase");

			pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
			{
				//if (ctx.Request.Headers.Keys.Contains("Origin") &&
				//		ctx.Request.Headers["Origin"].Any(x => x.StartsWith(sCORSAllowedOriginBase)))
				{
					ctx.Response.Headers["Access-Control-Allow-Origin"] = sCORSAllowedOriginBase;
					ctx.Response.Headers["Access-Control-Allow-Credentials"] = "true";
					ctx.Response.Headers["Access-Control-Allow-Headers"] = "Authorization, Content-Type";

					if ( (!ctx.Request.Method.Equals("GET") && 
							!(ctx.Request.Method.Equals("POST") || 
									ctx.Request.Headers["Content-Type"].Equals("application/x-www-form-urlencoded") ||
									ctx.Request.Headers["Content-Type"].Equals("multipart/form-data") ||
									ctx.Request.Headers["Content-Type"].Equals("text/plain"))) ||
							ctx.UsesCustomHeader() 
					)
					{
						// handle CORS preflight request
						ctx.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, PATCH, OPTIONS";
						ctx.Response.Headers["Access-Control-Expose-Headers"] = "X-Auth-Token, Authorization, Access-Control-Allow-Origin, Access-Control-Allow-Credentials";
						/*
						if (ctx.Request.Headers.Keys.Contains("Access-Control-Request-Headers"))
						{
							var allowedHeaders = "" + string.Join(
									", ", ctx.Request.Headers["Access-Control-Request-Headers"], "Access-Control-*, Origin, X-Requested-With, Content-Type, Accept");
							ctx.Response.Headers["Access-Control-Allow-Headers"] = allowedHeaders;
						}
						*/

					}
				}
			});
		}

		public static bool UsesCustomHeader(this Nancy.NancyContext ctx)
		{
			return ctx.Request.Headers.Any(x => x.Key.StartsWith("X", StringComparison.InvariantCultureIgnoreCase));
		}
	}
}
