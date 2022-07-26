using System;
using System.Collections;
using System.Collections.Generic;

namespace ChainRunner;

public class HandlerRegistry<TRequest> : IEnumerable<Type>
{
    private readonly HashSet<Type> _handlerTypes = new();

    public IEnumerator<Type> GetEnumerator() => _handlerTypes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Register<THandler>() where THandler : IResponsibilityHandler<TRequest>
    {
        _handlerTypes.Add(typeof(THandler));
    }
}