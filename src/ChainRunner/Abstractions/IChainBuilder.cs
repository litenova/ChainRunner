namespace ChainRunner
{
    public interface IChainBuilder
    {
        public IChainBuilder<TRequest> For<TRequest>();
    }

    public interface IChainBuilder<TRequest>
    {
        public IChainBuilder<TRequest> WithHandler<THandler>() where THandler : IResponsibilityHandler<TRequest>;
        
    }
}