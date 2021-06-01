using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChainRunner.Abstractions;

namespace ChainRunner.Registry
{
    internal class ChainRegistry : IChainRegistry
    {
        private readonly HashSet<ResponsibilityHandlerDescriptor> _descriptors = new();

        public void Register<THandler, TRequest>() where THandler : IResponsibilityHandler<TRequest>
        {
            _descriptors.Add(new ResponsibilityHandlerDescriptor
            {
                Handler = typeof(THandler),
                Request = typeof(TRequest),
            });
        }

        public IEnumerable<Type> GetHandlers<TRequest>()
        {
            return _descriptors.Where(p => p.Request == typeof(TRequest))
                               .Select(p => p.Handler);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<IResponsibilityHandlerDescriptor> GetEnumerator() => _descriptors.GetEnumerator();
    }
}