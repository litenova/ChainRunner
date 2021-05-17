using System;

namespace Buildy.Chain
{
    public interface IChainStarter<TRequest>
    {
        IChain<TRequest> StartsWith<THandler>() where THandler : IResponsibilityHandler<TRequest>;
    }
    
    public class ChainStarter<TRequest> : IChainStarter<TRequest>
    {
        private readonly IServiceProvider _serviceProvider;

        public ChainStarter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChain<TRequest> StartsWith<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            var chain = new Chain<TRequest>(_serviceProvider);

            return chain.Then<THandler>();
        }
    }
}