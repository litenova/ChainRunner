using System.Threading.Tasks;

namespace Buildy
{
    public interface IResponsibilityHandler<in TRequest>
    {
        Task HandleAsync(TRequest request);
    }
}