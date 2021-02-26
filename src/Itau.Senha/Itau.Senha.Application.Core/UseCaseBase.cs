using Itau.Senha.Domain.Core.Tracing;
using Itau.Senha.Domain.SeedWork;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Itau.Senha.Application.Core
{
    public abstract class UseCaseBase<TRequest, TResponse> : IUseCase<TRequest, TResponse> where TRequest : IRequest where TResponse : class, IResponse, new()
    {
        protected readonly ILogger<UseCaseBase<TRequest, TResponse>> _logger;
        private readonly Stopwatch _timer;
        protected readonly ITrace _trace;

        public UseCaseBase(ILogger<UseCaseBase<TRequest, TResponse>> logger, ITrace trace)
        {
            _trace = trace;
            _logger = logger;
            _timer = new Stopwatch();
        }
        public async Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken = default)
        {
            TResponse response;
            try
            {
                _logger.LogTrace("Iniciando validação");
                var erros = request.Validate();
                _logger.LogTrace("Finalizando validação");

                if (erros.Any())
                {
                    response = (TResponse)Activator.CreateInstance(typeof(TResponse));
                    response.AddErros(erros);
                    return response;
                }

                _logger.LogTrace("Iniciando execução");
                response = await InternalExecute(request, cancellationToken);
                _logger.LogTrace("Finalizando execução");
                _timer.Stop();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $" - Ocorreu um erro durante o processamento  - {ex.Message}");
                response = (TResponse)Activator.CreateInstance(typeof(TResponse));
                response.AddError(new ValidationError("api", "Ocorreu um erro durante o processamento da requisição"));
            }

            return response;
        }
        protected abstract Task<TResponse> InternalExecute(TRequest request, CancellationToken cancellationToken);
    }
}
