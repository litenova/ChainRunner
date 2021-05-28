namespace ChainRunner.Abstractions.Builder
{
    public interface IChainBuilder
    {
        IFirstHandlerBuildStep<TRequest> For<TRequest>();
    }
}