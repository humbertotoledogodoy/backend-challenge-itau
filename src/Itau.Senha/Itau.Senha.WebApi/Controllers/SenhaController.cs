using Itau.Senha.Application.Senha.ValidarSenha;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Itau.Senha.WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Senha")]
    [ApiController]
    public class SenhaController : ControllerBase
    {
        private readonly IValidarSenhaUseCase _validarSenhaCasoDeUso;
        public SenhaController(IValidarSenhaUseCase validarSenhaCasoDeUso)
        {
            _validarSenhaCasoDeUso = validarSenhaCasoDeUso;
        }

        [HttpPost]
        [Route("validar-senha")]
        public async Task<ActionResult<ValidarSenhaResponse>> ValidarSenha([Required, FromBody] ValidarSenhaRequest request)
        {
            var response = await _validarSenhaCasoDeUso.Execute(request);
            return response.SenhaValida ? Ok(response) : (ActionResult)BadRequest(new { erros = response.Errors });
        }
    }
}