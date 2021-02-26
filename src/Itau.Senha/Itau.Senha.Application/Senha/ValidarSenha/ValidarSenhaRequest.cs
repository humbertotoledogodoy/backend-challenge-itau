using Itau.Senha.Domain.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Itau.Senha.Application.Senha.ValidarSenha
{
    public class ValidarSenhaRequest : IRequest
    {
        [Required]
        public string Senha { get; set; }

        public IEnumerable<ValidationError> Validate()
        {
            var erros = new List<ValidationError>();

            if (string.IsNullOrEmpty(Senha))
               erros.Add(new ValidationError("Senha", "Senha inválida."));

            return erros;
        }
    }
}
