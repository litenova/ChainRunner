using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.UnitTests.Data
{
    public class FirstFakeChainHandler : IResponsibilityHandler<FakeChainRequest>
    {
        public Task HandleAsync(FakeChainRequest request, CancellationToken cancellationToken = default)
        {
            request.ExecutionLogs.Add("1");
            return Task.CompletedTask;
        }
    }
}