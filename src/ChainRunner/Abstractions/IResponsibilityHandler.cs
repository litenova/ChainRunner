using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner
{
    public interface IResponsibilityHandler
    {
        
    }
    
    public interface IResponsibilityHandler<in TRequest> : IResponsibilityHandler
    {
        Task HandleAsync(TRequest request,
                         IChainContext chainContext,
                         CancellationToken cancellationToken = default);
    }
}