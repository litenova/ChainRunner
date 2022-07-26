using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.UnitTests.Data;

public class ThirdFakeResponsibilityHandler : IResponsibilityHandler<FakeChainRequest>
{
    public Task HandleAsync(FakeChainRequest request,
                            IChainContext chainContext,
                            CancellationToken cancellationToken = default)
    {
        request.ExecutionLogs.Add("3");
        return Task.CompletedTask;
    }
}