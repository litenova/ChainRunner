

using System.Collections.Generic;

namespace ChainRunner
{
    public class ChainBuilder<TRequest>
    {
        private readonly HashSet<IResponsibilityHandler<TRequest>> _handlers = new();

        public ChainBuilder<TRequest> WithHandler<THandler>(THandler instance)
            where THandler : IResponsibilityHandler<TRequest>
        {
            _handlers.Add(instance);

            return this;
        }

        public ChainBuilder<TRequest> WithHandler<THandler>()
            where THandler : IResponsibilityHandler<TRequest>, new()
        {
            _handlers.Add(new THandler());

            return this;
        }
        
        public Chain<TRequest> Build()
        {
            return new Chain<TRequest>(_handlers);
        }
    }
}