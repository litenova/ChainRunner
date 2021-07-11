using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChainRunner
{
    public class ChainConfiguration<TRequest>
    {
        private readonly IServiceCollection _services;
        private readonly LazyHandlerRegistry<TRequest> _lazyHandlerRegistry;

        internal ChainConfiguration(IServiceCollection services, LazyHandlerRegistry<TRequest> lazyHandlerRegistry)
        {
            _services = services;
            _lazyHandlerRegistry = lazyHandlerRegistry;
        }

        public ChainConfiguration<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            _lazyHandlerRegistry.Register<THandler>();
            _services.TryAddTransient(typeof(THandler));
            _services.TryAddTransient(typeof(Lazy<THandler>), provider =>
            {
                var handler = provider.GetService<THandler>() ?? throw new InvalidOperationException();
                                       
                return new Lazy<IResponsibilityHandler<TRequest>>(handler);
            });

            return this;
        }
    }
}