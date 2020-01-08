using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Repository;
using GraphQL.Types;
using System;

namespace Einstein.WebAPI.Types
{
  public class EmployeeType : ObjectGraphType<Employee>
  {
	 public EmployeeType(IEmployeeRepository employeeRepository)
	 {
		Field(x => x.Id, nullable: false, type: typeof(IdGraphType));

		Field(x => x.EmployeeNumber);
		Field(x => x.Firstname);
		Field(x => x.Lastname);
		Field(x => x.Email);
		Field(x => x.FunctionTitle);
		Field(x => x.FunctionLevel);
		Field(x => x.EmployedSince, nullable: true);
		Field(x => x.AvailabilityPerWeek);
		Field(x => x.Manager, nullable: true, type: typeof(EmployeeType));
	 }
  }
}
