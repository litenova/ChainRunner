namespace ChainRunner.Internal;

internal class ChainContext : IChainContext
{
    public ChainContext()
    {
        Data = new ChainDataCollection();
    }

    public IChainDataCollection Data { get; }
}