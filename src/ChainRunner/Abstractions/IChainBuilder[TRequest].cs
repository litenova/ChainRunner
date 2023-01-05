namespace ChainRunner;

public interface IChainBuilder<TRequest>
{
    IChainBuilder<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>;

    IChain<TRequest> Build();
}