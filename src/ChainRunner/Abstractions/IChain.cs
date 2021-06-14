using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner
{
    public interface IChain<in TRequest>
    {
        Task RunAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}