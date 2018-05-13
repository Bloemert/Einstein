///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping
{
    public interface IMapper<in TSource, out TDestination>
    {
        TDestination Map(TSource source);
    }
}