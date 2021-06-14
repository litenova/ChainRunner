namespace ChainRunner
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