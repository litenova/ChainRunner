using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChainRunner
{
    public static class ServiceCollectionExtensions
    {
        public static ChainConfiguration<TRequest> AddChain<TRequest>(this IServiceCollection services)
        {
            LazyHandlerRegistry<TRequest> registry = new();

            services.AddTransient<IChain<TRequest>>(sp =>
            {
                var handlers = registry
                    .Select(sp.GetService)
                    .Cast<Lazy<IResponsibilityHandler<TRequest>>>();

                return new LazyChain<TRequest>(handlers);
            });
            
            return new ChainConfiguration<TRequest>(services, registry);
        }

        public static IServiceCollection AddResponsibilityHandlers(this IServiceCollection services,
                                                                   params Assembly[] assemblies)
        {
            foreach (var typeInfo in assemblies.SelectMany(a => a.DefinedTypes))
            {
                foreach (var @interface in typeInfo.ImplementedInterfaces)
                {
                    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IResponsibilityHandler<>))
                    {
                        services.TryAddTransient(typeInfo);
                        break;
                    }
                }
            }

            return services;
        }
    }
}