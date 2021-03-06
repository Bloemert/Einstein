﻿using AutoMapper;
using System;
///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping.AutoMapper
{
    public abstract class MappingProfile<TSource, TDestination> : Profile, IMappingProfile<TSource, TDestination>
    {
        protected MappingProfile()
        {
            IMappingExpression<TSource, TDestination> map = CreateMap<TSource, TDestination>();
            ConfigureMapping(map);
        }

        public virtual IMapper<TSource, TDestination> CreateMapper(IMapper mapper)
        {
            return new Mapper(mapper);
        }

        protected abstract void ConfigureMapping(IMappingExpression<TSource, TDestination> map);

        public class Mapper : IMapper<TSource, TDestination>
        {
            protected readonly IMapper mapper;

            public Mapper(IMapper mapper)
            {
                if (mapper == null)
                {
                    throw new ArgumentNullException("mapper");
                }

                this.mapper = mapper;
            }

            public virtual TDestination Map(TSource source)
            {
                return mapper.Map<TSource, TDestination>(source);
            }
        }
    }
}