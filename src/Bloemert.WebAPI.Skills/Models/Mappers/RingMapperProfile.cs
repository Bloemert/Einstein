using AutoMapper;
using Bloemert.Data.Entity.Auth.Entity;
using Bloemert.Data.Entity.Auth.Repository;
using Bloemert.Data.Entity.Skills.Entity;
using Bloemert.Lib.Auto.Mapping.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Skills.Models.Mappers
{
	public class RingMapperProfile : TwoWayMappingProfile<Ring, RingModel>
	{
		protected override void ConfigureMapping(IMappingExpression<RingModel, Ring> map)
		{
			//map.ForMember(dest => dest.PasswordData, opt => opt.MapFrom(source => Users.HashPassword(source.NewPassword)));
		}

		protected override void ConfigureMapping(IMappingExpression<Ring, RingModel> map)
		{
			// Properties with matching names are mapped automatically.
			//map.ForMember(dest => dest.NewPassword, opt => opt.Ignore());
		}
	}
}
