using Bloemert.Data.Core;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace Bloemert.Lib.WebAPI
{
	public class BaseModule<T, R, E, M> : NancyModule
		where R : IRepository<E>
		where E : BaseEntity
	{

		public IRepository<E> Repository { get; set; }

		public ITwoWayMapper<E, M> Mapper { get; set; }

		public BaseModule(
				IAppConfig appCfg,
				IRepository<E> repository,
				ITwoWayMapper<E, M> mapper,
				string modulePath = "")
			: base(appCfg.GetValue(@"BaseModulePath:Value") + modulePath)
		{
			// Authentication for all requests!
			this.RequiresAuthentication();

			// Authorisation for all requests!
			Before += ctx =>
			{
				Response response = new Nancy.Response();

				if (ctx.CurrentUser != null &&
						!ctx.CurrentUser.HasClaim(ClaimTypes.System, typeof(E).Name + "_" + ctx.Request.Method))
				{
					return null;
				}

				return response.WithStatusCode(HttpStatusCode.Forbidden);
			};

			Repository = repository;
			Mapper = mapper;

			Get("/new", args =>
			{
				return Negotiate
								.WithModel(new ModelWrapper<M> { Data = Mapper.Map(Repository.NewEntity()) });
			});

			Get("/{id}/get", args =>
			{
				return Negotiate
								.WithModel(new ModelWrapper<M> { Data = Mapper.Map(Repository.GetEntity((int)args.id)) });
			});

			Get("/list", args =>
			{
				return Negotiate
								.WithModel(new ModelWrapper<IList<M>> { Data = (IList<M>)Mapper.Map(Repository.ListEntity()) });
			});


			Put("/{id}/save", args =>
			{
				ModelWrapper<M> model = this.Bind<ModelWrapper<M>>();

				E entity = Mapper.Map(model.Data);
				return Negotiate
								.WithModel(new ModelWrapper<M> { Data = Mapper.Map(Repository.SaveEntity(entity)) });
			});


			Delete("/{id}/delete", args =>
			{
				return Negotiate
								.WithModel(new ModelWrapper<bool> { Data = Repository.DeleteEntity((int)args.id) });
			});

			//Options("/*", args =>
			//{
			//	Response response = new Nancy.Response();

			//	return response.WithStatusCode(HttpStatusCode.OK);
			//});
		}
	}
}
