using Itau.Senha.Application.Core;
using Itau.Senha.Domain.Core.Tracing;
using Itau.Senha.Domain.SeedWork;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Itau.Senha.Application.Senha.ValidarSenha
{
    public class ValidarSenhaUseCase : UseCaseBase<ValidarSenhaRequest, ValidarSenhaResponse>, IValidarSenhaUseCase
    {
        public ValidarSenhaUseCase(ILogger<UseCaseBase<ValidarSenhaRequest, ValidarSenhaResponse>> logger,
                                   ITrace trace) : base(logger, trace)
        {
        }

        protected override async Task<ValidarSenhaResponse> InternalExecute(ValidarSenhaRequest request, CancellationToken cancellationToken)
        {
            var response = new ValidarSenhaResponse();

            //Valida se existem caracteres duplicados na string
            var checkDoubleCaracter = request.Senha.GroupBy(x => x).Any(g => g.Count() > 1);
            if (checkDoubleCaracter)
            {
                response.AddError(new ValidationError("Senha", $"Senha inválida."));
                return response;
            }

            //Valida se a senha condiz com a regra: Ao menos 1 uma letra minuscula, ao menos uma letra maiuscula, ao menos um digito, ao menos um caracter especial considerando os seguintes caracteres: !@#$%^&*()-+, no minimo com 9 caracteres
            Match rulePassword = Regex.Match(request.Senha, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-+])[A-Za-z\d!@#$%^&*()-+]{9,100}$", RegexOptions.IgnoreCase);
            if (!rulePassword.Success)
            {
                response.AddError(new ValidationError("Senha", $"Senha inválida."));
                return response;
            }
            
            response.SenhaValida = true;
            return await Task.Run(() => response).ConfigureAwait(false);
        }
    }
}
