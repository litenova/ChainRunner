using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner.Abstractions
{
    public interface IChain<TRequest>
    {
        Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
    
    
}