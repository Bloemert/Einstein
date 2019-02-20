using Bloemert.Data.Core;
using Bloemert.Lib.Config;

using System;

using Serilog;
using Microsoft.AspNetCore.Mvc;
using Bloemert.Data.Entity.Auth.Entity;
using NHibernate;
using EntityGraphQL.Schema;
using EntityGraphQL;
using System.Net.Http;
using System.Net;
using Einstein.WebAPI.GraphQL;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Controllers
{

	[Route("api/[controller]")]
	public class QueryController : ControllerBase
	{
		private ILogger Log { get; }

		private ISessionFactory FNhDbSessionFactory { get; set; }
		private MappedSchemaProvider<DataContext> SchemaProvider { get; }

		public QueryController(
				ISessionFactory fnhDbSessionFactory,
				MappedSchemaProvider<DataContext> schemaProvider,
				ILogger log)
		{

			FNhDbSessionFactory = fnhDbSessionFactory;
			SchemaProvider = schemaProvider;
			Log = log;
		}

		[HttpGet]
		public async Task<string> GetAsync()
		{
			return SchemaProvider.GetGraphQLSchema();
		}

		[HttpPost]
		public async Task<object> PostAsync([FromBody]string query)
		{
			try
			{
				return await Task.Run(() => { return FNhDbSessionFactory.QueryObject(query, SchemaProvider); });
			}
			catch (Exception ex)
			{
				HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
				message.Content = new StringContent("Error in query: " + ex.Message);
				return message;
			}
		}
	}
}

