using AutoMapper;
///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping.AutoMapper
{
    public abstract class TwoWayMappingProfile<TSource, TDestination> : MappingProfile<TSource, TDestination>, ITwoWayMappingProfile<TSource, TDestination>
    {
        protected TwoWayMappingProfile()
        {
            IMappingExpression<TDestination, TSource> destinationToSourceMap = CreateMap<TDestination, TSource>();
            ConfigureMapping(destinationToSourceMap);
        }

        public new ITwoWayMapper<TSource, TDestination> CreateMapper(IMapper mapper)
        {
            return new TwoWayMapper(mapper);
        }

        protected abstract void ConfigureMapping(IMappingExpression<TDestination, TSource> map);

        public class TwoWayMapper : Mapper, ITwoWayMapper<TSource, TDestination>
        {
            public TwoWayMapper(IMapper mapper)
                : base(mapper)
            {
            }

            public virtual TSource Map(TDestination source)
            {
                return mapper.Map<TDestination, TSource>(source);
            }
        }
    }
}