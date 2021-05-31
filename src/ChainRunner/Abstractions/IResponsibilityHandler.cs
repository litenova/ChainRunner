using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.Abstractions
{
    public interface IResponsibilityHandler<in TRequest>
    {
        Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}