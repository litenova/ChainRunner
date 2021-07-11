using System;
using System.Collections;
using System.Collections.Generic;

namespace ChainRunner
{
    public class LazyHandlerRegistry<TRequest> : IEnumerable<Type>
    {
        private readonly HashSet<Type> _lazyHandlerTypes = new();

        public IEnumerator<Type> GetEnumerator() => _lazyHandlerTypes.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Register<THandler>() where THandler : IResponsibilityHandler<TRequest>
        {
            _lazyHandlerTypes.Add(typeof(Lazy<THandler>));
        }
    }
}