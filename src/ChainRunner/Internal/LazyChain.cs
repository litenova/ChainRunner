using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner
{
    internal class LazyChain<TRequest> : IChain<TRequest>
    {
        private readonly IEnumerable<Lazy<IResponsibilityHandler<TRequest>>> _handlers;

        public LazyChain(IEnumerable<Lazy<IResponsibilityHandler<TRequest>>> handlers)
        {
            _handlers = handlers;
        }

        public async Task RunAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            var chainContext = new ChainContext();
            
            foreach (var handler in _handlers)
            {
                await handler.Value.HandleAsync(request, chainContext, cancellationToken);
            }
        }
    }
}