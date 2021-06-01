using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChainRunner.Abstractions;
using ChainRunner.Exceptions;

namespace ChainRunner.Chain
{
    public class Chain<TRequest> : IChain<TRequest>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IChainRegistry _chainRegistry;

        public Chain(IServiceProvider serviceProvider, IChainRegistry chainRegistry)
        {
            _serviceProvider = serviceProvider;
            _chainRegistry = chainRegistry;
        }

        public async Task RunAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            foreach (var handlerType in _chainRegistry.GetHandlers<TRequest>())
            {
                var handler = _serviceProvider.GetService(handlerType) as IResponsibilityHandler<TRequest>;

                if (handler is null)
                {
                    throw new HandlerNotRegisteredException(handlerType);
                }
                
                await handler.HandleAsync(request, cancellationToken);
            }
        }
    }
}