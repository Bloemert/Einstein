using Autofac;
using AutoMapper;
using System.Collections.Generic;
///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping.AutoMapper.Autofac
{
    /// <summary>
    /// Factory class for creating AutoMapper configuration using profiles registered to Autofac container.
    /// </summary>
    public class AutofacMapperConfigurationFactory : MapperConfigurationFactory
    {
        /// <summary>
        /// Creates AutoMapper configuration using profiles registered to Autofac container.
        /// </summary>
        /// <param name="componentContext">Autofac component context.</param>
        public MapperConfiguration CreateMapperConfiguration(IComponentContext componentContext)
        {
            IEnumerable<Profile> profiles = componentContext.Resolve<IEnumerable<Profile>>();
            return CreateMapperConfiguration(profiles);
        }
    }
}