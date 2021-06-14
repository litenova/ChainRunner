using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChainRunner
{
    public static class ServiceCollectionExtensions
    {
        public static ChainConfiguration<TRequest> AddChain<TRequest>(this IServiceCollection services)
        {
            services.TryAddSingleton(ChainRegistryAccessor.ChainRegistry);
            services.AddTransient<IChain<TRequest>>(sp => new Chain<TRequest>(sp, sp.GetRequiredService<IChainRegistry>()));

            return new ChainConfiguration<TRequest>(services, ChainRegistryAccessor.ChainRegistry);
        }
    }
}