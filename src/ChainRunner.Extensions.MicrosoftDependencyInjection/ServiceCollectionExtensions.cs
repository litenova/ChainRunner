using System;
using ChainRunner.Abstractions;
using ChainRunner.Abstractions.Builder;
using ChainRunner.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ChainRunner.Extensions.MicrosoftDependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddChain<TRequest>(this IServiceCollection services,
                                                            Func<IChainBuilder, IChain<TRequest>> chainBuilder)
        {
            services.AddTransient<IChainBuilder, ChainBuilder>();
            
            return services;
        }
    }
}