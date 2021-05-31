using System;
using ChainRunner.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ChainRunner.Extensions.MicrosoftDependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IChainBuilder<TRequest> AddChain<TRequest>(this IServiceCollection services)
        {
            var chainBuilder = new ChainBuilder<TRequest>(services);

            services.AddSingleton(chainBuilder);
            services.AddTransient(typeof(IChain<TRequest>), provider =>
            {
                var chainBuilder = provider.GetService(typeof(ChainBuilder<TRequest>)) as ChainBuilder<TRequest>;
                return chainBuilder.Build(provider);
            });

            return chainBuilder;
        }
    }
}