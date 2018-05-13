///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping
{
    public interface ITwoWayMapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        TSource Map(TDestination source);
    }
}