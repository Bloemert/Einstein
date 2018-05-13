///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping
{
    public interface IMapperFactory
    {
        IMapper<TSource, TDestination> Create<TSource, TDestination>();
    }
}