using Einstein.DataAccessLayer.Entities;
using Einstein.DataAccessLayer.Repository;
using GraphQL.Authorization;
using GraphQL.Types;
using System;

namespace Einstein.WebAPI.Types
{
  public class UserType : ObjectGraphType<User>
  {
	 public UserType(IUserRepository userRepository)
	 {
		this.AuthorizeWith("AdminPolicy");

		Field(x => x.Id, nullable: false, type: typeof(IdGraphType));

		Field(x => x.Active);
		Field(x => x.UserName);
		Field(x => x.Firstname);
		Field(x => x.Lastname);
		Field(x => x.Email);
		Field(x => x.Employee, nullable: true, type: typeof(EmployeeType));
	 }
  }
}
