using System;
using System.Collections;
using System.Collections.Generic;

namespace ChainRunner
{
    internal class ChainContext : IChainContext
    {
        public ChainContext()
        {
            Data = new ChainDataCollection();
        }

        public IChainDataCollection Data { get; }
    }
}