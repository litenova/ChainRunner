using System;
using System.Collections.Generic;

namespace ChainRunner
{
    internal class LazyChainBuilder : IChainBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        public LazyChainBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChainBuilder<TRequest> For<TRequest>()
        {
            return new LazyChainBuilder<TRequest>(_serviceProvider);
        }
    }

    internal class LazyChainBuilder<TRequest> : IChainBuilder<TRequest>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly List<Lazy<IResponsibilityHandler<TRequest>>> _handlers = new();

        public LazyChainBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChainBuilder<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            var handler = _serviceProvider.GetService(typeof(Lazy<THandler>));

            if (handler is null) throw new HandlerNotRegisteredException(typeof(THandler));
            
            _handlers.Add(handler as Lazy<IResponsibilityHandler<TRequest>> ?? throw new InvalidOperationException());

            return this;
        }

        public IChainBuilder<TRequest> WithHandler<THandler>(THandler instance) where THandler : IResponsibilityHandler<TRequest>
        {
            _handlers.Add(new Lazy<IResponsibilityHandler<TRequest>>(instance));

            return this;
        }

        public IChain<TRequest> Build()
        {
            return new LazyChain<TRequest>(_handlers);
        }
    }
}