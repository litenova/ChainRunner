using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner
{
    public interface IResponsibilityHandler<in TRequest>
    {
        Task HandleAsync(TRequest request,
                         IChainContext chainContext,
                         CancellationToken cancellationToken = default);
    }
}