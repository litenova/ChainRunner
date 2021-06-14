using System;
using System.Collections.Generic;


namespace ChainRunner
{
    public interface IChainRegistry : IEnumerable<IResponsibilityHandlerDescriptor>
    {
        void Register<THandler, TRequest>() where THandler : IResponsibilityHandler<TRequest>;
    }
}