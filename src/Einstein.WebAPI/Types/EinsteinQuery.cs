using Autofac;
using Einstein.DataAccessLayer.Repository;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Types
{
  public class EinsteinQuery : ObjectGraphType<object>
  {
	 public EinsteinQuery(ILifetimeScope ioc)
	 {
		Name = "Query";

		FieldAsync(
  name: "Users",
  description: "Einstein's Users",
  type: typeof(ListGraphType<UserType>),
  resolve: async context =>
  {
	 var repository = ioc.Resolve<IUserRepository>();

	 return await repository.ListQueryAsync();
  });
	 }

  }
}
