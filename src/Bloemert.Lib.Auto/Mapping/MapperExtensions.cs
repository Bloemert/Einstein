﻿using System.Collections.Generic;
using System.Linq;
///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping
{
    public static class MapperExtensions
    {
        public static IReadOnlyCollection<TDestination> Map<TSource, TDestination>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> sourceCollection)
        {
            return sourceCollection == null ? new List<TDestination>(0) : sourceCollection.Select(mapper.Map).ToList();
        }

        public static IReadOnlyCollection<TSource> Map<TSource, TDestination>(this ITwoWayMapper<TSource, TDestination> mapper, IEnumerable<TDestination> sourceCollection)
        {
            return sourceCollection == null ? new List<TSource>(0) : sourceCollection.Select(mapper.Map).ToList();
        }
    }
}