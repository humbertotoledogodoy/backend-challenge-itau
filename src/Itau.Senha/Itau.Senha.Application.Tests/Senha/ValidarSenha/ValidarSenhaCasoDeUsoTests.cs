using FluentAssertions;
using Itau.Senha.Application.Core;
using Itau.Senha.Application.Senha.ValidarSenha;
using Itau.Senha.Domain.Core.Tracing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Itau.Senha.Application.Tests.Senha.ValidarSenha
{
    [TestFixture(Description = "")]

    public class ValidarSenhaCasoDeUsoTests
    {
        private readonly Mock<ITrace> _traceMock = new Mock<ITrace>();
        private readonly Mock<ILogger<UseCaseBase<ValidarSenhaRequest, ValidarSenhaResponse>>> _loggerMock = new Mock<ILogger<UseCaseBase<ValidarSenhaRequest, ValidarSenhaResponse>>>();
        private ValidarSenhaUseCase validarSenhaUseCase;

        [OneTimeSetUp]
        public virtual void OneTimeSetUpAsync()
        {
            validarSenhaUseCase = new ValidarSenhaUseCase(_loggerMock.Object, _traceMock.Object);
        }

        [Test]
        [Description("Quando A Senha For Valida O Resultado SenhaValida Deve Ser De Sucesso")]
        public async Task Quando_A_Senha_For_Valida_O_Resultado_SenhaValida_Deve_Ser_De_Sucesso()
        {
            //arrange
            var validarSenhaRequest = new ValidarSenhaRequest() { Senha = "AbTp9!fok" };

            //act
            var result = await validarSenhaUseCase.Execute(validarSenhaRequest);

            //assert
            result.SenhaValida.Should().BeTrue();
        }

        [Test]
        [Description("Quando O Request Nao For Preenchido O Resultado SenhaValida Deve Ser De Erro")]
        public async Task Quando_O_Request_Nao_For_Preenchido_O_Resultado_SenhaValida_Deve_Ser_De_Erro()
        {
            //arrange
            var validarSenhaRequest = new ValidarSenhaRequest() { };

            //act
            var result = await validarSenhaUseCase.Execute(validarSenhaRequest);

            //assert
            result.SenhaValida.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Error == "Senha inválida.");
        }

        [Test]
        [Description("Quando Senha For vazia O Resultado SenhaValida Deve Ser De Erro")]
        public async Task Quando_Senha_For_vazia_O_Resultado_SenhaValida_Deve_Ser_De_Erro()
        {
            //arrange
            var validarSenhaRequest = new ValidarSenhaRequest() { Senha = ""};

            //act
            var result = await validarSenhaUseCase.Execute(validarSenhaRequest);

            //assert
            result.SenhaValida.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Error == "Senha inválida.");
        }

        [Test]
        [Description("Quando Senha Conter Somente Caracteres Minusculos E Duplicados O Resultado SenhaValida Deve Ser De Erro")]
        public async Task Quando_Senha_Conter_Somente_Caracteres_Minusculos_E_Duplicados_O_Resultado_SenhaValida_Deve_Ser_De_Erro()
        {
            //arrange
            var validarSenhaRequest = new ValidarSenhaRequest() { Senha = "aa" };

            //act
            var result = await validarSenhaUseCase.Execute(validarSenhaRequest);

            //assert
            result.SenhaValida.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Error == "Senha inválida.");
        }

        [Test]
        [Description("Quando Senha Conter Somente Caracteres Minusculos O Resultado SenhaValida Deve Ser De Erro")]
        public async Task Quando_Senha_Conter_Somente_Caracteres_Minusculos_O_Resultado_SenhaValida_Deve_Ser_De_Erro()
        {
            //arrange
            var validarSenhaRequest = new ValidarSenhaRequest() { Senha = "ab" };

            //act
            var result = await validarSenhaUseCase.Execute(validarSenhaRequest);

            //assert
            result.SenhaValida.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Error == "Senha inválida.");
        }

        [Test]
        [Description("Quando Senha Conter Caracteres Maiusculos Minusculos E Duplicados O Resultado SenhaValida Deve Ser De Erro")]
        public async Task Quando_Senha_Conter_Caracteres_Maiusculos_Minusculos_E_Duplicados_O_Resultado_SenhaValida_Deve_Ser_De_Erro()
        {
            //arrange
            var validarSenhaRequest = new ValidarSenhaRequest() { Senha = "AAAbbbCc" };

            //act
            var result = await validarSenhaUseCase.Execute(validarSenhaRequest);

            //assert
            result.SenhaValida.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Error == "Senha inválida.");
        }

        [Test]
        [Description("Quando Senha Conter Caracteres Duplicados O Resultado SenhaValida Deve Ser De Erro")]
        public async Task Quando_Senha_Conter_Caracteres_Duplicados_O_Resultado_SenhaValida_Deve_Ser_De_Erro()
        {
            //arrange
            var validarSenhaRequest = new ValidarSenhaRequest() { Senha = "AbTp9!foo" };

            //act
            var result = await validarSenhaUseCase.Execute(validarSenhaRequest);

            //assert
            result.SenhaValida.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Error == "Senha inválida.");
        }

        [Test]
        [Description("Quando Senha Nao Conter Ao Menos Um Caracter Especial O Resultado SenhaValida Deve Ser De Erro")]
        public async Task Quando_Senha_Nao_Conter_Ao_Menos_Um_Caracter_Especial_O_Resultado_SenhaValida_Deve_Ser_De_Erro()
        {
            //arrange
            var validarSenhaRequest = new ValidarSenhaRequest() { Senha = "AbTp9 fok" };

            //act
            var result = await validarSenhaUseCase.Execute(validarSenhaRequest);

            //assert
            result.SenhaValida.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Error == "Senha inválida.");
        }

        [Test]
        [Description("Quando O Request For Nulo O Resultado Deve Ser De Erro")]
        public async Task Quando_O_Request_For_Nulo_O_Resultado_Deve_Ser_De_Erro()
        {
            //act
            var result = await validarSenhaUseCase.Execute(null);

            //assert
            result.SenhaValida.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Error == "Ocorreu um erro durante o processamento da requisição");
        }
    }
}
