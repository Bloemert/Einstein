using GraphQL.Server.Transports.AspNetCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Auth
{
  public class GraphQLUserContextBuilder : IUserContextBuilder
  {
	 public Task<object> BuildUserContext(HttpContext httpContext) =>
		  Task.FromResult<object>(new GraphQLUserContext() { User = httpContext.User });
  }
}
