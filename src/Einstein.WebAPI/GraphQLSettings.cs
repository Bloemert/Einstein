using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einstein.WebAPI
{
  public class GraphQLSettings
  {
	 public PathString Path { get; set; } = "/api/graphql";

	 public Func<HttpContext, IDictionary<string, object>> BuildUserContext { get; set; }

	 public bool EnableMetrics { get; set; }
	 public List<IValidationRule> ValidationRules { get; internal set; } = new List<IValidationRule>();
  }
}
