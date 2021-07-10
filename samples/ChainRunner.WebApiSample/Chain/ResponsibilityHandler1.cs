using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.WebApiSample.Chain
{
    public class ResponsibilityHandler1 : IResponsibilityHandler<ChainRequest>
    {
        
        public Task HandleAsync(ChainRequest request, 
                                IChainContext chainContext,
                                CancellationToken cancellationToken = default)
        {
            Debug.WriteLine($"Running {nameof(ResponsibilityHandler1)}!");

            return Task.CompletedTask;
        }
    }
}