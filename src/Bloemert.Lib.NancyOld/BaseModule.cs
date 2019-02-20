using Bloemert.Data.Core;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Carter;
using System.Threading.Tasks;
using Carter.Response;
using Carter.ModelBinding;
using Serilog;

namespace Bloemert.Lib.WebAPI
{
	public class BaseModule<T, R, E, M> : CarterModule
		where R : IRepository<E>
		where E : BaseEntity
	{
		private ILogger Log { get; }

		public IRepository<E> Repository { get; set; }

		public ITwoWayMapper<E, M> Mapper { get; set; }

		public BaseModule(
				IAppConfig appCfg,
				IRepository<E> repository,
				ITwoWayMapper<E, M> mapper,
				IUserIdentityProvider identityProvider,
				ILogger log,
				string modulePath)
			: base(appCfg.GetValue(@"BaseModulePath:Value") + modulePath)
		{
			Log = log;

			// Authentication for all requests!
			this.RequiresAuthentication();

			// Authorisation for all requests!
			Before = (ctx) =>
			{
				if (ctx.User != null &&
						!ctx.User.HasClaim(ClaimTypes.System, typeof(E).Name + "_" + ctx.Request.Method))
				{
					identityProvider.ClaimsPrincipal = ctx.User;

					return new Task<bool>(() => true);
				}

				return new Task<bool>(() => false);
			};

			Repository = repository;
			Mapper = mapper;

			Get("/new", async (req, res, routeData) =>
			{
				try
				{
					await res.Negotiate(new ModelWrapper<M> { Data = Mapper.Map(Repository.NewEntity()) });
				}
				catch ( Exception ex )
				{
					Log.Error("Error /new: {ex}", ex);
				}
			});

			Get("/{id}/get", async (req, res, routeData) =>
			{
				await res.Negotiate(new ModelWrapper<M> { Data = Mapper.Map(Repository.GetEntity((Guid)routeData.Values["id"])) });
			});

			Get("/list", async (req, res, routeData) =>
			{
				try
				{
					await res.Negotiate(new ModelWrapper<IList<M>> { Data = (IList<M>)Mapper.Map(Repository.ListQuery()) });
				}
				catch ( Exception ex )
				{
					await res.Negotiate(new ModelWrapper<IList<M>> { Error = new ModelWrapperError { Message = ex.Message, Details = ex.StackTrace } });
				}
			});


			Put("/{id}/save", async (req, res, routeData) =>
			{
				ModelWrapper<M> model = req.Bind<ModelWrapper<M>>();

				E entity = Mapper.Map(model.Data);
				await res.Negotiate(new ModelWrapper<M> { Data = Mapper.Map(Repository.SaveEntity(entity)) });
			});


			Delete("/{id}/delete", async (req, res, routeData) =>
			{
				await res.Negotiate(new ModelWrapper<bool> { Data = Repository.DeleteEntity((Guid)routeData.Values["id"]) });
			});

		}
	}
}
