using AutoMapper;
///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping.AutoMapper
{
    public interface IMappingProfile<in TSource, out TDestination>
    {
        IMapper<TSource, TDestination> CreateMapper(IMapper mapper);
    }
}