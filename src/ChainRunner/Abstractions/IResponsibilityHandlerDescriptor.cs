using System;

namespace ChainRunner
{
    public interface IResponsibilityHandlerDescriptor
    {
        Type HandlerType { get; }
        Type RequestType { get; }
    }
}