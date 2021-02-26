using Itau.Senha.Domain.Core.Tracing;
using Microsoft.AspNetCore.Http;

namespace Itau.Senha.WebApi.Middlewares
{
    public class TraceMiddleware : ITrace
    {
        public TraceMiddleware()
        {
        }
    }
}
