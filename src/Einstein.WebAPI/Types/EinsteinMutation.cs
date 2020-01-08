using Autofac;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Types
{
  public class EinsteinMutation : ObjectGraphType
  {
	 public EinsteinMutation(ILifetimeScope ioc)
	 {
			Name = "Mutation";

	 }
  }
}
