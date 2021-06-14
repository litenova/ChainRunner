using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner
{
    public interface IResponsibilityHandler<in TRequest>
    {
        Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}