using System;
using System.Collections.Generic;
using ChainRunner.Abstractions;
using ChainRunner.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ChainRunner.Extensions.MicrosoftDependencyInjection
{
    public interface IChainBuilder<TRequest>
    {
        IChainBuilder<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>;
    }

    internal class ChainBuilder<TRequest> : IChainBuilder<TRequest>
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly List<Type> _handlerTypes = new();

        public ChainBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public IChainBuilder<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            _serviceCollection.AddTransient(typeof(THandler));
            _handlerTypes.Add(typeof(THandler));
            
            return this;
        }
        
        public IChain<TRequest> Build(IServiceProvider serviceProvider)
        {
            return new Chain<TRequest>(serviceProvider, _handlerTypes.ToArray());
        }
    }
}