using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChainRunner;

public class ChainConfiguration<TRequest>
{
    private readonly IServiceCollection _services;
    private readonly HandlerRegistry<TRequest> _handlerRegistry;

    internal ChainConfiguration(IServiceCollection services, HandlerRegistry<TRequest> handlerRegistry)
    {
        _services = services;
        _handlerRegistry = handlerRegistry;
    }

    public ChainConfiguration<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>
    {
        _handlerRegistry.Register<THandler>();
        _services.TryAddTransient(typeof(THandler));

        return this;
    }
}