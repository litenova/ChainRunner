using System;
using ChainRunner.Abstractions.Builder;

namespace ChainRunner.Internal
{
    public class ChainBuilder : IChainBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        public ChainBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFirstHandlerBuildStep<TRequest> For<TRequest>()
        {
            return new ChainStepBuilder<TRequest>(_serviceProvider);
        }
    }
}