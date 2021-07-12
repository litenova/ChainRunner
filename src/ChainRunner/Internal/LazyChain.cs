using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner
{
    internal class LazyChain<TRequest> : IChain<TRequest>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<Type> _handlerTypes;

        public LazyChain(IServiceProvider serviceProvider,
                         IEnumerable<Type> handlerTypes)
        {
            _serviceProvider = serviceProvider;
            _handlerTypes = handlerTypes;
        }

        public async Task RunAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            var chainContext = new ChainContext();

            foreach (var handlerType in _handlerTypes)
            {
                var handler = _serviceProvider.GetService(handlerType) as IResponsibilityHandler<TRequest>;

                if (handler is null) throw new HandlerNotRegisteredException(handlerType);

                await handler.HandleAsync(request, chainContext, cancellationToken);
            }
        }
    }
}