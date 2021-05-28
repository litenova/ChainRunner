using System;

namespace ChainRunner.Exceptions
{
    [Serializable]
    public class HandlerNotRegisteredException : Exception
    {
        public HandlerNotRegisteredException(Type handlerType) : base($"handler of type '{handlerType}' is not registered")
        {
            
        }
    }
}