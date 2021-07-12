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
            HandlerRegistry<TRequest> registry = new();

            services.AddSingleton<IChain<TRequest>>(sp => new LazyChain<TRequest>(sp, registry));

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

            var handlerTypes = assemblies.SelectMany(a => a.DefinedTypes)
                                         .Where(t => t.ImplementedInterfaces.Contains(typeof(IResponsibilityHandler)));

            foreach (var typeInfo in handlerTypes)
            {
                services.TryAddTransient(typeInfo);
            }

            return services;
        }
    }
}