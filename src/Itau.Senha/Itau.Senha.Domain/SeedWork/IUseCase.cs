using System.Threading;
using System.Threading.Tasks;

namespace Itau.Senha.Domain.SeedWork
{
    public interface IUseCase<TRequest, TResponse> where TRequest : IRequest where TResponse : class, IResponse, new()
    {
        Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken = default);
    }

    public interface IUseCase<TResponse> where TResponse : class, IResponse, new()
    {
        Task<TResponse> Execute(CancellationToken cancellationToken = default);
    }
}
