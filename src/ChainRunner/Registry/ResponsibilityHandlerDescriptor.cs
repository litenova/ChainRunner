using System;

namespace ChainRunner
{
    internal class ResponsibilityHandlerDescriptor : IResponsibilityHandlerDescriptor
    {
        public Type HandlerType { get; set; }
        public Type RequestType { get; set; }
        public object Instance { get; set; }
    }
}