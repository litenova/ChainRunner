namespace ChainRunner.Abstractions.Builder
{
    public interface IChainHandlersBuildStep<TRequest>
    {
        IChainHandlersBuildStep<TRequest> Then<THandler>() where THandler : IResponsibilityHandler<TRequest>;
        IChainFinalBuildStep<TRequest> FinalizeWith<THandler>() where THandler : IResponsibilityHandler<TRequest>;
        IChain<TRequest> Build();
    }
}