using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Einstein.WebAPI.Models;
using Einstein.WebAPI.Types;
using GraphQL;
using GraphQL.Execution;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Einstein.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
	[Authorize]
  public class GraphQLController : ControllerBase
  {
	 ISchema EinsteinSchema { get; }

	 public GraphQLController(ISchema schema)
	 {
		EinsteinSchema = schema;
	 }

	 [HttpPost]
	 public async Task<ActionResult> Post([FromBody] GraphQLQuery query)
	 {
		ExecutionResult result = null;

		try
		{
		  var inputs = query.Variables.ToInputs();

		  result = await new DocumentExecuter().ExecuteAsync(_ =>
		  {
			 _.Schema = EinsteinSchema;
			 _.Query = query.Query;
			 _.OperationName = query.OperationName;
			 _.Inputs = inputs;
		  });
		}
		catch (Exception ex)
		{
		  return BadRequest(ex.Message);
		}

		return Ok(new { data = result?.Data, errors = result?.Errors });
	 }
  }

}