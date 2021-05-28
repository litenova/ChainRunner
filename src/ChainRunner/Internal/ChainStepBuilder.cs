using System;
using System.Collections.Generic;
using ChainRunner.Abstractions;
using ChainRunner.Abstractions.Builder;

namespace ChainRunner.Internal
{
    internal class ChainStepBuilder<TRequest> : IFirstHandlerBuildStep<TRequest>,
                                                IChainHandlersBuildStep<TRequest>,
                                                IChainFinalBuildStep<TRequest>
    {
        private readonly HashSet<Type> _handlerTypes = new();
        private Type? _finalizeHandler;
        private readonly IServiceProvider _serviceProvider;

        public ChainStepBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChainHandlersBuildStep<TRequest> StartWith<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            Then<THandler>();

            return this;
        }

        public IChainHandlersBuildStep<TRequest> Then<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            _handlerTypes.Add(typeof(TRequest));

            return this;
        }

        public IChainFinalBuildStep<TRequest> FinalizeWith<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            _finalizeHandler = typeof(THandler);

            return this;
        }

        IChain<TRequest> IChainHandlersBuildStep<TRequest>.Build() => Build();

        IChain<TRequest> IChainFinalBuildStep<TRequest>.Build() => Build();

        private IChain<TRequest> Build()
        {
            return new Chain<TRequest>(_handlerTypes, _finalizeHandler, _serviceProvider);
        }
    }
}