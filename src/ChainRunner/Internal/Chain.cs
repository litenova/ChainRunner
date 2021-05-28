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
        private readonly HashSet<Type> _handlerTypes;
        private readonly Type _finalizeHandlerType;
        private readonly IServiceProvider _serviceProvider;

        public Chain(HashSet<Type> handlerTypes, Type? finalizeHandlerType, IServiceProvider serviceProvider)
        {
            _handlerTypes = handlerTypes;
            _finalizeHandlerType = finalizeHandlerType;
            _serviceProvider = serviceProvider;
        }

        public async Task HandleAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            var handlers = ResolveHandlers();
            var finalizeHandler = ResolveFinalizeHandler();

            foreach (var responsibilityHandler in handlers)
            {
                try
                {
                    await responsibilityHandler.HandleAsync(request, cancellationToken);
                }
                finally
                {
                    await finalizeHandler.HandleAsync(request, cancellationToken);
                }
            }
            
            await finalizeHandler.HandleAsync(request, cancellationToken);
        }

        private IEnumerable<IResponsibilityHandler<TRequest>> ResolveHandlers()
        {
            foreach (var handlerType in _handlerTypes)
            {
                var handler = _serviceProvider.GetService(handlerType);

                if (handler is null)
                {
                    throw new HandlerNotRegisteredException(handlerType);
                }

                yield return (IResponsibilityHandler<TRequest>) handler;
            }
        }

        private IResponsibilityHandler<TRequest> ResolveFinalizeHandler()
        {
            var finalizeHandler = _serviceProvider.GetService(_finalizeHandlerType);

            if (finalizeHandler is null)
            {
                throw new HandlerNotRegisteredException(_finalizeHandlerType);
            }

            return (IResponsibilityHandler<TRequest>) finalizeHandler;
        }
    }
}