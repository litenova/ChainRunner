using System.Collections.Generic;

namespace ChainRunner.UnitTests.Data
{
    public class FakeChainRequest
    {
        public List<string> ExecutionLogs { get; set; } = new List<string>();
    }
}