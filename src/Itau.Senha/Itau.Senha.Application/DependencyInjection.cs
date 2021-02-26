using Itau.Senha.Application.Senha.ValidarSenha;
using Microsoft.Extensions.DependencyInjection;

namespace Itau.Senha.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidarSenhaUseCase, ValidarSenhaUseCase>();
            return services;
        }
    }
}
