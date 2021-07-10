using System.Threading.Tasks;
using ChainRunner.UnitTests.Data;
using FluentAssertions;
using Xunit;

namespace ChainRunner.UnitTests
{
    public class ChainTests 
    {
        [Fact]
        public async Task chain_should_call_handlers_in_orders()
        {
            // Arrange
            var chain = new ChainBuilder<FakeChainRequest>()
                        .WithHandler<FirstFakeChainHandler>()
                        .WithHandler<SecondFakeChainHandler>()
                        .WithHandler<ThirdFakeChainHandler>()
                        .Build();

            var request = new FakeChainRequest();

            // Act
            await chain.RunAsync(request);
            
            // Assert
            request.ExecutionLogs.Should().HaveElementAt(0, "1");
            request.ExecutionLogs.Should().HaveElementAt(1, "2");
            request.ExecutionLogs.Should().HaveElementAt(2, "3");
        }
    }
}