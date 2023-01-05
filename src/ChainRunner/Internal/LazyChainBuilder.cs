using System;

namespace ChainRunner.Internal;

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