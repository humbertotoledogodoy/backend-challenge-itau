using Itau.Senha.Domain.SeedWork;

namespace Itau.Senha.Application.Senha.ValidarSenha
{
    public class ValidarSenhaResponse : BaseResponse
    {
        public bool SenhaValida { get; set; } = false;
    }
}
