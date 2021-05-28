namespace ChainRunner.Abstractions.Builder
{
    public interface IChainFinalBuildStep<TRequest>
    {
        IChain<TRequest> Build();
    }
}