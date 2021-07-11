using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChainRunner
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the specified <see cref="IChain{TRequest}"/>
        /// </summary>
        /// <param name="services">the service collection</param>
        /// <returns>the chain configuration</returns>
        public static ChainConfiguration<TRequest> AddChain<TRequest>(this IServiceCollection services)
        {
            LazyHandlerRegistry<TRequest> registry = new();

            services.AddSingleton<IChain<TRequest>>(sp =>
            {
                var handlers = registry
                               .Select(sp.GetService)
                               .Cast<Lazy<IResponsibilityHandler<TRequest>>>();

                return new LazyChain<TRequest>(handlers);
            });

            return new ChainConfiguration<TRequest>(services, registry);
        }

        /// <summary>
        /// Registers the <see cref="IChainBuilder"/> and responsibility handlers from specified assemblies
        /// </summary>
        /// <param name="services">the service collection</param>
        /// <param name="assemblies">the specified assemblies</param>
        /// <returns>the service collection</returns>
        public static IServiceCollection AddChainRunner(this IServiceCollection services,
                                                        params Assembly[] assemblies)
        {
            services.AddTransient<IChainBuilder, LazyChainBuilder>();
            
            foreach (var typeInfo in assemblies.SelectMany(a => a.DefinedTypes))
            {
                foreach (var @interface in typeInfo.ImplementedInterfaces)
                {
                    if (@interface.IsGenericType &&
                        @interface.GetGenericTypeDefinition() == typeof(IResponsibilityHandler<>))
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