using System;
using ChainRunner.Abstractions;

namespace ChainRunner.Registry
{
    internal class ResponsibilityHandlerDescriptor : IResponsibilityHandlerDescriptor
    {
        public Type Handler { get; set; }
        public Type Request { get; set; }

        public object Instance { get; set; }
    }
}