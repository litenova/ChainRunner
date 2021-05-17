using System;
using System.Threading.Tasks;

namespace Buildy.Chain
{
    public interface IChain<TRequest>
    {
        IChain<TRequest> Then<THandler>() where THandler : IResponsibilityHandler<TRequest>;
        Task RunAsync();
    }
    
    internal class Chain<TRequest> : IChain<TRequest>
    {
        private readonly IServiceProvider _serviceProvider;

        internal Chain(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChain<TRequest> Then<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            return this;
        }

        public Task RunAsync()
        {
            return Task.CompletedTask;
        }
    }
}