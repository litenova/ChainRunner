using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ChainRunner
{
    internal class ChainRegistry : IChainRegistry
    {
        private readonly HashSet<ResponsibilityHandlerDescriptor> _descriptors = new();

        public void Register<THandler, TRequest>() where THandler : IResponsibilityHandler<TRequest>
        {
            _descriptors.Add(new ResponsibilityHandlerDescriptor
            {
                HandlerType = typeof(THandler),
                RequestType = typeof(TRequest),
            });
        }

        public IEnumerable<Type> GetHandlers<TRequest>()
        {
            return _descriptors.Where(p => p.RequestType == typeof(TRequest))
                               .Select(p => p.HandlerType);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<IResponsibilityHandlerDescriptor> GetEnumerator() => _descriptors.GetEnumerator();
    }
}