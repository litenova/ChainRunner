using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.Abstractions
{
    public interface IResponsibilityHandler<TRequest>
    {
        Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}