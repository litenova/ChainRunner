using System;

namespace ChainRunner.Abstractions
{
    public interface IHandlerDescriptor
    {
        Type HandlerType { get; }

        Type RequestType { get; }

        object? Instance { get; }
    }

    internal class HandlerDescriptor : IHandlerDescriptor
    {
        public HandlerDescriptor(Type handlerType, Type requestType, object? instance = default)
        {
            HandlerType = handlerType;
            RequestType = requestType;
            Instance = instance;
        }

        public Type HandlerType { get; init; }
        public Type RequestType { get; init; }
        public object? Instance { get; init; }
    }
}