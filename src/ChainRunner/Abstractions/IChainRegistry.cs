using System;
using System.Collections.Generic;

namespace ChainRunner.Abstractions
{
    public interface IChainRegistry : IEnumerable<IResponsibilityHandlerDescriptor>
    {
        void Register<THandler, TRequest>() where THandler : IResponsibilityHandler<TRequest>;

        IEnumerable<Type> GetHandlers<TRequest>();
    }
}