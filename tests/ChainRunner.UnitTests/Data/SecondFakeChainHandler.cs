using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.UnitTests.Data
{
    public class SecondFakeChainHandler : IResponsibilityHandler<FakeChainRequest>
    {
        public Task HandleAsync(FakeChainRequest request, CancellationToken cancellationToken = default)
        {
            request.ExecutionLogs.Add("2");
            return Task.CompletedTask;
        }
    }
}