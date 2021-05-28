using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ChainRunner.Abstractions
{
    public interface IHandlerRegistry : IEnumerable<IHandlerDescriptor>
    {
        void Register<THandler, TRequest>() where THandler : IResponsibilityHandler<TRequest>;

        void RegisterFromAssembly(Assembly assembly);

        void Register<THandler, TRequest>(IResponsibilityHandler<TRequest> handlerInstance)
            where THandler : IResponsibilityHandler<TRequest>;
    }

    public class HandlerRegistry : IHandlerRegistry
    {
        private readonly List<HandlerDescriptor> _descriptors = new();

        public void Register<THandler, TRequest>() where THandler : IResponsibilityHandler<TRequest>
        {
            _descriptors.Add(new HandlerDescriptor(typeof(THandler), typeof(TRequest)));
        }

        public void RegisterFromAssembly(Assembly assembly)
        {
            foreach (var assemblyDefinedType in assembly.DefinedTypes)
            {
                foreach (var @interface in assemblyDefinedType.ImplementedInterfaces)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(IResponsibilityHandler<>))
                    {
                        var requestType = @interface.GetGenericParameterConstraints()[0];
                        
                        _descriptors.Add(new HandlerDescriptor(assemblyDefinedType, requestType));
                    }
                }
            }
        }

        public void Register<THandler, TRequest>(IResponsibilityHandler<TRequest> handlerInstance)
            where THandler : IResponsibilityHandler<TRequest>
        {
            _descriptors.Add(new HandlerDescriptor(typeof(THandler), typeof(TRequest), handlerInstance));
        }

        public IEnumerator<IHandlerDescriptor> GetEnumerator() => _descriptors.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}