using Microsoft.Extensions.DependencyInjection;

namespace ChainRunner
{
    public class ChainConfiguration<TRequest>
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly IChainRegistry _registry;

        internal ChainConfiguration(IServiceCollection serviceCollection, IChainRegistry registry)
        {
            _serviceCollection = serviceCollection;
            _registry = registry;
        }

        public ChainConfiguration<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            _serviceCollection.AddTransient(typeof(THandler));
            _registry.Register<THandler, TRequest>();

            return this;
        }
    }
}