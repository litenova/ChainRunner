using System.Threading.Tasks;

namespace Buildy.Chain
{
    public interface IResponsibilityHandler<in TRequest>
    {
        Task HandleAsync(TRequest request);
    }
}