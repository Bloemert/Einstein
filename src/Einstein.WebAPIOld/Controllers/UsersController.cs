using Bloemert.Data.Core;
using Bloemert.Lib.Config;

using System;
using System.Threading.Tasks;

using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Bloemert.Data.Entity.Auth.Entity;

namespace Einstein.WebAPI.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private ILogger Log { get; }

		public IRepository<User> Repository { get; set; }

		public UserController(
				IAppConfig appCfg,
				IRepository<User> repository,
				ILogger log)
		{
			Log = log;

			// Authentication for all requests!
			//this.RequiresAuthentication();

			// Authorisation for all requests!
			//Before = (ctx) =>
			//{
			//	if (ctx.User != null &&
			//			!ctx.User.HasClaim(ClaimTypes.System, typeof(E).Name + "_" + ctx.Request.Method))
			//	{
			//		identityProvider.ClaimsPrincipal = ctx.User;

			//		return new Task<bool>(() => true);
			//	}

			//	return new Task<bool>(() => false);
			//};

			Repository = repository;
		}

		[HttpGet("new")]
		public async Task<Document> NewAsync()
		{
			try
			{
				Uri currentRequestUri = new Uri(this.Request.GetDisplayUrl());

				var resourceData = await Repository.NewEntityAsync();

				// Build and POST JSON API document to create new article. 
				using (var documentContext = new EinsteinDocumentContext(currentRequestUri))
				{
					return documentContext.NewDocument(currentRequestUri)
														.SetJsonApiVersion(JsonApiVersion.Version10)
														.Links()
																.AddSelfLink()
														.LinksEnd()
														.Resource(resourceData)
															.Relationships()
																.AddRelationship("user", new[] { Keywords.Related })
															.RelationshipsEnd()
														.ResourceEnd()
													.WriteDocument();
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error NewAsync: {ex}", ex);

				throw ex;
			}
		}


		[HttpGet("/{id}")]
		public async Task<Document> GetAsync(Guid id)
		{
			try
			{
				Uri currentRequestUri = new Uri(this.Request.GetDisplayUrl());

				var resourceData = await Repository.GetEntityAsync(id);

				// Build and POST JSON API document to create new article. 
				using (var documentContext = new EinsteinDocumentContext(currentRequestUri))
				{
					return documentContext.NewDocument(currentRequestUri)
														.SetJsonApiVersion(JsonApiVersion.Version10)
														.Links()
																.AddSelfLink()
														.LinksEnd()
														.Resource(resourceData)
															.Relationships()
																.AddRelationship("user", new[] { Keywords.Related })
															.RelationshipsEnd()
														.ResourceEnd()
													.WriteDocument();
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error GetAsync: {ex}", ex);

				throw ex;
			}
		}

		[HttpGet("/")]
		public async Task<Document> ListAsync(Guid id)
		{
			try
			{
				Uri currentRequestUri = new Uri(this.Request.GetDisplayUrl());

				var resourceData = await Repository.ListQueryAsync();

				// Build and POST JSON API document to create new article. 
				using (var documentContext = new EinsteinDocumentContext(currentRequestUri))
				{
					return documentContext.NewDocument(currentRequestUri)
														.SetJsonApiVersion(JsonApiVersion.Version10)
														.Links()
																.AddSelfLink()
														.LinksEnd()
														.ResourceCollection(resourceData)
															.Relationships()
																.AddRelationship("user", new[] { Keywords.Related })
															.RelationshipsEnd()
														.ResourceCollectionEnd()
													.WriteDocument();
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error ListAsync: {ex}", ex);

				throw ex;
			}
		}


		[HttpPost("/")]
		public async Task<Document> CreateAsync([FromBody]Document inDocument)
		{
			try
			{
				Uri currentRequestUri = new Uri(this.Request.GetDisplayUrl());

				var resourceData = await Repository.SaveEntityAsync((User)inDocument.GetData());

				// Build and POST JSON API document to create new article. 
				using (var documentContext = new EinsteinDocumentContext(currentRequestUri))
				{
					return documentContext.NewDocument(currentRequestUri)
														.SetJsonApiVersion(JsonApiVersion.Version10)
														.Links()
																.AddSelfLink()
														.LinksEnd()
														.Resource(resourceData)
															.Relationships()
																.AddRelationship("user", new[] { Keywords.Related })
															.RelationshipsEnd()
														.ResourceEnd()
													.WriteDocument();
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error CreateAsync: {ex}", ex);

				throw ex;
			}
		}


		[HttpPatch("/{id}")]
		public async Task<Document> CreateAsync(Guid id, [FromBody]Document inDocument)
		{
			try
			{
				Uri currentRequestUri = new Uri(this.Request.GetDisplayUrl());

				var resourceData = await Repository.SaveEntityAsync((User)inDocument.GetData());
				if (!resourceData.Id.Equals(id))
				{
					throw new ArgumentException("Id as argument is different than in document!");
				}

				// Build and POST JSON API document to create new article. 
				using (var documentContext = new EinsteinDocumentContext(currentRequestUri))
				{
					return documentContext.NewDocument(currentRequestUri)
														.SetJsonApiVersion(JsonApiVersion.Version10)
														.Links()
																.AddSelfLink()
														.LinksEnd()
														.Resource(resourceData)
															.Relationships()
																.AddRelationship("user", new[] { Keywords.Related })
															.RelationshipsEnd()
														.ResourceEnd()
													.WriteDocument();
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error CreateAsync: {ex}", ex);

				throw ex;
			}
		}

		[HttpDelete("/{id}")]
		public async Task<Document> DeleteAsync(Guid id)
		{
			try
			{
				Uri currentRequestUri = new Uri(this.Request.GetDisplayUrl());

				var resourceData = await Repository.DeleteEntityAsync(id);

				// Build and POST JSON API document to create new article. 
				using (var documentContext = new EinsteinDocumentContext(currentRequestUri))
				{
					return documentContext.NewDocument(currentRequestUri)
														.SetJsonApiVersion(JsonApiVersion.Version10)
														.Links()
																.AddSelfLink()
														.LinksEnd()
													.WriteDocument();
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error DeleteAsync: {ex}", ex);

				throw ex;
			}
		}
	}
}
