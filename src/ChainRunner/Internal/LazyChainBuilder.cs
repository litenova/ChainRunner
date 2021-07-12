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
        private readonly HashSet<Type> _handlerTypes = new();

        public LazyChainBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChainBuilder<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            _handlerTypes.Add(typeof(THandler));

            return this;
        }

        public IChain<TRequest> Build()
        {
            return new LazyChain<TRequest>(_serviceProvider, _handlerTypes);
        }
    }
}