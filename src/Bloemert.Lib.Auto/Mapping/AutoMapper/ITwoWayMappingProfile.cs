using AutoMapper;
///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping.AutoMapper
{
    public interface ITwoWayMappingProfile<TSource, TDestination>
    {
        ITwoWayMapper<TSource, TDestination> CreateMapper(IMapper mapper);
    }
}