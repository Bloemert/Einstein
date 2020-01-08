using Autofac;
using GraphQL;
using GraphQL.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Einstein.WebAPI.Auth
{
  public class JwtTokenAuthorizationEvaluator : IAuthorizationEvaluator
  {
	 private readonly IHttpContextAccessor _httpContextAccessor;
	 private readonly IWebHostEnvironment _environment;
	 private readonly AuthorizationSettings _authorizationSettings;

	 public JwtTokenAuthorizationEvaluator(
			IHttpContextAccessor httpContextAccessor,
		  IWebHostEnvironment environment,
		  AuthorizationSettings authorizationSettings)
	 {
		_httpContextAccessor = httpContextAccessor;
		_environment = environment;
		_authorizationSettings = authorizationSettings;
	 }

	 public async Task<AuthorizationResult> Evaluate(ClaimsPrincipal principal, object userContext, Dictionary<string, object> arguments, IEnumerable<string> requiredPolicies)
	 {
		var cxt = userContext as GraphQLUserContext;
		ClaimsPrincipal claimsPrincipal = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ? _httpContextAccessor.HttpContext.User : null;
		if (claimsPrincipal == null)
		{
		  var response = await _httpContextAccessor.HttpContext.AuthenticateAsync("Bearer");
		  if (response.Succeeded)
		  {
			 claimsPrincipal = response.Principal;
			 _httpContextAccessor.HttpContext.User = response.Principal;
		  }
		  else
		  {
			 return AuthorizationResult.Fail(new List<string> { "Not Authorized!" });
		  }
		}

		var context = new AuthorizationContext();
		context.User = claimsPrincipal;
		context.UserContext = userContext;
		context.Arguments = arguments;

		var authPolicies = _authorizationSettings.GetPolicies(requiredPolicies);
		var tasks = new List<Task>();
		authPolicies.Apply(p =>
		{
		  p.Requirements.Apply(r =>
		  {
			 var task = r.Authorize(context);
			 tasks.Add(task);
		  });
		});

		await Task.WhenAll(tasks.ToArray());
		return !context.HasErrors
		? AuthorizationResult.Success()
		: AuthorizationResult.Fail(context.Errors);
	 }
  }
}
