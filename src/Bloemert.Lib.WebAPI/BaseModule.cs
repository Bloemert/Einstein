using Bloemert.Data.Core;
using Bloemert.Lib.Auto.Mapping;
using Bloemert.Lib.Config;

using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
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
				IUserIdentityProvider identityProvider,
				string modulePath)
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
					identityProvider.ClaimsPrincipal = ctx.CurrentUser;

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
				try
				{
					return Negotiate
								.WithModel(new ModelWrapper<IList<M>> { Data = (IList<M>)Mapper.Map(Repository.ListEntity()) });
				}
				catch ( Exception ex )
				{
					return Negotiate
								.WithModel(new ModelWrapper<IList<M>> { Error = new ModelWrapperError { Message = ex.Message, Details = ex.StackTrace } });
				}
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

		}
	}
}
