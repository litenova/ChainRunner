using System;

namespace Buildy.Chain
{
    public interface IChainRunner
    {
        IChainStarter<TRequest> For<TRequest>();
    }
    
    public class ChainRunner : IChainRunner
    {
        private readonly IServiceProvider _serviceProvider;

        public ChainRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChainStarter<TRequest> For<TRequest>()
        {
            return new ChainStarter<TRequest>(_serviceProvider);
        }
    }

    // public class Temp : IResponsibilityHandler<int>
    // {
    //     public Temp()
    //     {
    //         ChainRunner.For<int>()
    //              .StartsWith<Temp>()
    //              .Then<Temp>()
    //              .Then<Temp>()
    //              .RunAsync();
    //     }
    //
    //     public Task HandleAsync(int request)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
}