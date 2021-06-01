using System;
using ChainRunner.Abstractions;
using ChainRunner.Chain;
using ChainRunner.Registry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChainRunner.Extensions.MicrosoftDependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static ChainConfiguration<TRequest> AddChain<TRequest>(this IServiceCollection services)
        {
            services.TryAddSingleton(ChainRegistryAccessor.ChainRegistry);
            services.AddTransient<IChain<TRequest>, Chain<TRequest>>();

            return new ChainConfiguration<TRequest>(services, ChainRegistryAccessor.ChainRegistry);
        }
    }
}