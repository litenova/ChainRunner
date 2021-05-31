using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChainRunner.Abstractions;
using ChainRunner.Exceptions;

namespace ChainRunner.Internal
{
    public class Chain<TRequest> : IChain<TRequest>
    {
        private readonly IEnumerable<Type> _handlerTypes;
        private readonly IServiceProvider _serviceProvider;

        public Chain(IServiceProvider serviceProvider, IEnumerable<Type> handlerTypes)
        {
            _handlerTypes = handlerTypes;
            _serviceProvider = serviceProvider;
        }

        public async Task RunAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            foreach (var handlerType in _handlerTypes)
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