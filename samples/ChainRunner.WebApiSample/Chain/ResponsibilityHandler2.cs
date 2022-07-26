using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.WebApiSample.Chain;

public class ResponsibilityHandler2 : IResponsibilityHandler<ChainRequest>
{
    public Task HandleAsync(ChainRequest request,
                            IChainContext chainContext,
                            CancellationToken cancellationToken = default)
    {
        Debug.WriteLine($"Running {nameof(ResponsibilityHandler2)}!");

        return Task.CompletedTask;
    }
}