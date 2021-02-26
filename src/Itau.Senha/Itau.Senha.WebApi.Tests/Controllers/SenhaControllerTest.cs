using FluentAssertions;
using Itau.Senha.Application.Senha.ValidarSenha;
using Itau.Senha.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace Itau.Senha.WebApi.Tests.Controllers
{
    public class SenhaControllerTest
    {
        private readonly Mock<IValidarSenhaUseCase> validarSenhaUseCase = new Mock<IValidarSenhaUseCase>();

        [Test]
        public async System.Threading.Tasks.Task SenhaControllerValida()
        {
            validarSenhaUseCase.Setup(x => x.Execute(It.IsAny<ValidarSenhaRequest>(), default)).ReturnsAsync(new ValidarSenhaResponse {  SenhaValida = true});
            var senhaController = new SenhaController(validarSenhaUseCase.Object);
            var result = await senhaController.ValidarSenha(new ValidarSenhaRequest() { Senha = "AbTp9!fok" });
            ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.Value.Should().Be(200);
            ((Domain.SeedWork.BaseResponse)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value).Errors.Count.Should().Be(0);
        }

        [Test]
        public async System.Threading.Tasks.Task SenhaControllerInvalida()
        {
            validarSenhaUseCase.Setup(x => x.Execute(It.IsAny<ValidarSenhaRequest>(), default)).ReturnsAsync(new ValidarSenhaResponse { SenhaValida = false });
            var senhaController = new SenhaController(validarSenhaUseCase.Object);
            var result = await senhaController.ValidarSenha(new ValidarSenhaRequest() { Senha = "" });
            ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.Value.Should().Be(400);
        }
    }
}
