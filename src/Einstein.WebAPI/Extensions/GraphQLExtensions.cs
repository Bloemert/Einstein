using Einstein.WebAPI.Auth;
using GraphQL.Authorization;
using GraphQL.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Extensions
{
  public static class GraphQLExtensions
  {
	 public static void AddGraphQLAuth(this IServiceCollection services)
	 {
		services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.TryAddSingleton<IAuthorizationEvaluator, JwtTokenAuthorizationEvaluator>();
		services.AddTransient<IValidationRule, AuthorizationValidationRule>();

		services.AddSingleton(
						  x =>
						  {
							 var authorizationSettings = new AuthorizationSettings();
							 authorizationSettings.AddPolicy(
								  "AdminPolicy",
								  y => y.RequireClaim("role", "Admin"));
							 return authorizationSettings;
						  });
	 }

		public static void UseGraphQLWithAuth(this IApplicationBuilder app)
	 {
		//var rules = app.ApplicationServices.GetServices<IValidationRule>();
		//settings.ValidationRules.AddRange(rules);

		//app.UseMiddleware<GraphQLMiddleware>(settings);
	 }
  }
}
