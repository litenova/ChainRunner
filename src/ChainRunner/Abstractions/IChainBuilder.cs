namespace ChainRunner;

/// <summary>
/// Allows building chains on the fly
/// </summary>
public interface IChainBuilder
{
    IChainBuilder<TRequest> For<TRequest>();
}