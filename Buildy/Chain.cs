using System;
using System.Threading.Tasks;

namespace Buildy
{
    public class Chain
    {
        public static ChainStarter<TRequest> For<TRequest>()
        {
            return new ChainStarter<TRequest>();
        }
    }

    public class ChainStarter<TRequest>
    {
        public Chain<TRequest> StartsWith<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            return new Chain<TRequest>();
        }
    }

    public class Chain<TRequest>
    {
        internal Chain()
        {
            
        }

        public Chain<TRequest> Then<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            return this;
        }

        public Task RunAsync()
        {
            return Task.CompletedTask;
        }
    }

    public class Temp : IResponsibilityHandler<int>
    {
        public Temp()
        {
            Chain.For<int>()
                 .StartsWith<Temp>()
                 .Then<Temp>()
                 .Then<Temp>()
                 .RunAsync();
        }

        public Task HandleAsync(int request)
        {
            throw new NotImplementedException();
        }
    }
}