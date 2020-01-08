using GraphQL.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Auth
{
  public class GraphQLUserContext : IProvideClaimsPrincipal
  {
	 public ClaimsPrincipal User { get; set; }
  }
}
