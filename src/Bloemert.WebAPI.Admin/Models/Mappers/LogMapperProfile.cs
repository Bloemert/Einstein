using AutoMapper;
using Bloemert.Data.Entity.Admin.Entity;
using Bloemert.Data.Entity.Admin.Repository;
using Bloemert.Lib.Auto.Mapping.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.WebAPI.Admin.Models.Mappers
{
	public class LogMapperProfile : TwoWayMappingProfile<DBLog, LogModel>
	{
		protected override void ConfigureMapping(IMappingExpression<LogModel, DBLog> map)
		{
		}

		protected override void ConfigureMapping(IMappingExpression<DBLog, LogModel> map)
		{
		}
	}
}
