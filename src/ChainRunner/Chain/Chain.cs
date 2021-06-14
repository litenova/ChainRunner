using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner
{
    public class Chain<TRequest> : IChain<TRequest>
    {
        private readonly IEnumerable<IResponsibilityHandler<TRequest>> _handlers;

        public Chain(IEnumerable<IResponsibilityHandler<TRequest>> handlers)
        {
            _handlers = handlers;
        }

        public Chain(IServiceProvider serviceProvider, IChainRegistry chainRegistry)
        {
            _handlers = chainRegistry.Where(descriptor => descriptor.RequestType == typeof(TRequest))
                                     .Select(descriptor =>
                                     {
                                         var handler = serviceProvider.GetService(descriptor.HandlerType);

                                         if (handler is null)
                                         {
                                             throw new HandlerNotRegisteredException(descriptor.HandlerType);
                                         }

                                         return handler as IResponsibilityHandler<TRequest>;
                                     })!;
        }

        public async Task RunAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            foreach (var handler in _handlers)
            {
                await handler.HandleAsync(request, cancellationToken);
            }
        }
    }
}