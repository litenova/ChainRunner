using System.Threading;
using System.Threading.Tasks;

namespace ChainRunner;

/// <summary>
/// Represents a chain of responsibility
/// </summary>
/// <typeparam name="TRequest">The request type</typeparam>
public interface IChain<in TRequest>
{
    /// <summary>
    /// Runs the chain of responsibilities
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns></returns>
    Task RunAsync(TRequest request, CancellationToken cancellationToken = default);
}