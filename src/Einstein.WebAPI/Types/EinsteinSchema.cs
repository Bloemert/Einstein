using Autofac;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQL.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Types
{
  public class EinsteinSchema : GraphQL.Types.Schema
  {

	 public static void RegisterGraphQLTypes(ContainerBuilder builder)
	 {

		builder.RegisterInstance(new DocumentExecuter()).As<IDocumentExecuter>();
		builder.RegisterInstance(new DocumentWriter()).As<IDocumentWriter>();
		
		builder.RegisterType<EinsteinQuery>().AsSelf();
		builder.RegisterType<UserType>().AsSelf();
		builder.RegisterType<EmployeeType>().AsSelf();


		builder.RegisterType<EinsteinMutation>().AsSelf();

		builder.RegisterType<EinsteinSchema>().As<ISchema>().InstancePerLifetimeScope();


		builder.Register<GraphQL.IDependencyResolver>(c =>
		{
		  var context = c.Resolve<IComponentContext>();
		  return new FuncDependencyResolver(type => context.Resolve(type));
		});
	 }



	 public EinsteinSchema(
			IComponentContext context, 
			EinsteinQuery query,
			EinsteinMutation mutation,
			ILogger log)
				: base(new FuncDependencyResolver(type => (GraphType)context.Resolve(type)))
	 {
		Query = query;
		Mutation = mutation;
		log.Information("Loading Einstein schema...");
	 }
  }
}
