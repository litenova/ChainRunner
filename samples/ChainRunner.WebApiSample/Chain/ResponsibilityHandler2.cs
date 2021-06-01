using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ChainRunner.Abstractions;

namespace ChainRunner.WebApiSample.Chain
{
    public class ResponsibilityHandler2 : IResponsibilityHandler<ChainRequest>
    {
        public Task HandleAsync(ChainRequest request, CancellationToken cancellationToken = default)
        {
            Debug.WriteLine($"Running {nameof(ResponsibilityHandler2)}!");
            
            return Task.CompletedTask;
        }
    }
}