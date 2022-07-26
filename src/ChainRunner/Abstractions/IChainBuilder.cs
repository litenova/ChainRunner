namespace ChainRunner;

/// <summary>
/// Allows building chains on the fly
/// </summary>
public interface IChainBuilder
{
    IChainBuilder<TRequest> For<TRequest>();
}

public interface IChainBuilder<TRequest>
{
    IChainBuilder<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>;

    IChain<TRequest> Build();
}