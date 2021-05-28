namespace ChainRunner.Abstractions.Builder
{
    public interface IFirstHandlerBuildStep<TRequest>
    {
        IChainHandlersBuildStep<TRequest> StartWith<THandler>() where THandler : IResponsibilityHandler<TRequest>;
    }
}