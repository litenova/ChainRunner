using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.Internal;

internal class Chain<TRequest> : IChain<TRequest>
{
    private readonly IEnumerable<IResponsibilityHandler<TRequest>> _handlers;

    public Chain(IEnumerable<IResponsibilityHandler<TRequest>> handlers)
    {
        _handlers = handlers;
    }

    public async Task RunAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        var chainContext = new ChainContext();

        foreach (var handler in _handlers)
        {
            await handler.HandleAsync(request, chainContext, cancellationToken);
        }
    }
}