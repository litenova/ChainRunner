using ChainRunner.Abstractions;

namespace ChainRunner.Registry
{
    public static class ChainRegistryAccessor
    {
        public static IChainRegistry ChainRegistry { get; }

        static ChainRegistryAccessor()
        {
            ChainRegistry = new ChainRegistry();
        }
    }
}