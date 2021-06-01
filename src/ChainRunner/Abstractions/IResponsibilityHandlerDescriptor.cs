using System;

namespace ChainRunner.Abstractions
{
    public interface IResponsibilityHandlerDescriptor
    {
        Type Handler { get; }

        Type Request { get; }

        // object Instance { get; }
    }
}