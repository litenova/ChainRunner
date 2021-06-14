using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner
{
    public class ResponsibilityHandler3 : IResponsibilityHandler<ChainRequest>
    {
        public Task HandleAsync(ChainRequest request, CancellationToken cancellationToken = default)
        {
            Debug.WriteLine($"Running {nameof(ResponsibilityHandler3)}!");
            
            return Task.CompletedTask;
        }
    }
}