using Autofac;
using System;
///
/// Source	: Affecto.Mapping
/// Url			:	https://github.com/affecto/dotnet-Mapping
///
namespace Bloemert.Lib.Auto.Mapping.AutoMapper.Autofac
{
    public class AutofacMapperFactory : IMapperFactory
    {
        protected readonly IComponentContext componentContext;

        public AutofacMapperFactory(IComponentContext componentContext)
        {
            if (componentContext == null)
            {
                throw new ArgumentNullException(nameof(componentContext));
            }
            this.componentContext = componentContext;
        }

        public virtual IMapper<TSource, TDestination> Create<TSource, TDestination>()
        {
            return componentContext.Resolve<IMapper<TSource, TDestination>>();
        }
    }
}