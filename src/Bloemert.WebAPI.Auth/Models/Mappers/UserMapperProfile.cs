using AutoMapper;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Repository;
using Bloemert.Lib.Auto.Mapping.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Auth.Models.Mappers
{
	public class UserMapperProfile : TwoWayMappingProfile<User, UserModel>
	{
		public IUserRepository Users { get; set; }

		//public UserMapperProfile(IUserRepository users)
		//{
		//	this.Users = users;

		//	//IMappingExpression<User, UserModel> sourceToDestinationMap = CreateMap<User, UserModel>();
		//	//this.ConfigureMapping(sourceToDestinationMap);

		//	//IMappingExpression<UserModel, User> destinationToSourceMap = CreateMap<UserModel, User>();
		//	//this.ConfigureMapping(destinationToSourceMap);
		//}

		protected override void ConfigureMapping(IMappingExpression<UserModel, User> map)
		{
			map.ForMember(dest => dest.PasswordData, opt => opt.MapFrom(source => Users.HashPassword(source.NewPassword)));
		}

		protected override void ConfigureMapping(IMappingExpression<User, UserModel> map)
		{
			// Properties with matching names are mapped automatically.
			map.ForMember(dest => dest.NewPassword, opt => opt.Ignore());
		}
	}
}
