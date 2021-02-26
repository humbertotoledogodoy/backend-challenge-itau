using FluentAssertions;
using Itau.Senha.Application.Senha.ValidarSenha;
using Itau.Senha.Integrations.Tests.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Itau.Senha.Integrations.Tests.Controllers
{

    public class SenhaControlerTests : BaseTest
    {
    
       
        [Test]
        public async Task QuandoRequestSenhaForValidoDeveRetornar200ESenhaValidaVerdadeira()
        {
            //arrange
            var request = new ValidarSenhaRequest()
            {
                Senha = "AbTp9!fok"
            };

            //act
            var result = await Client.PostAsync($"Senha/validar-senha", request);
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValidarSenhaResponse>(content);

            //assert
            result.StatusCode.Should().Be(200);
            response.SenhaValida.Should().BeTrue();
            response.Sucesso.Should().BeTrue();

        }

        [Test]
        public async Task QuandoRequestSenhaForVazioDeveRetornarStatus400ESenhaValidaFalsa()
        {
            //arrange
            var request = new ValidarSenhaRequest()
            {
                Senha = ""
            };

            //act
            var result = await Client.PostAsync($"Senha/validar-senha", request);
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValidarSenhaResponse>(content);

            //assert
            result.StatusCode.Should().Be(400);
            response.Sucesso.Should().BeTrue();
            response.SenhaValida.Should().BeFalse();
        }

        [Test]
        public async Task QuandoRequestSenhaConterCaracterDuplicadoDeveRetornarStatus400ESenhaValidaFalsa()
        {
            //arrange
            var request = new ValidarSenhaRequest()
            {
                Senha = "aa"
            };

            //act
            var result = await Client.PostAsync($"Senha/validar-senha", request);
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValidarSenhaResponse>(content);

            //assert
            result.StatusCode.Should().Be(400);
            response.Sucesso.Should().BeTrue();
            response.SenhaValida.Should().BeFalse();
        }
        
        [Test]
        public async Task QuandoRequestSenhaInvalidoRetornarStatus400ESenhaValidaFalsa()
        {
            //arrange
            var request = new ValidarSenhaRequest()
            {
                Senha = "AbTp9!fo"
            };

            //act
            var result = await Client.PostAsync($"Senha/validar-senha", request);
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValidarSenhaResponse>(content);

            //assert
            result.StatusCode.Should().Be(400);
            response.SenhaValida.Should().BeFalse();
            response.Sucesso.Should().BeTrue();

        }

        [Test]
        public async Task QuandoRotaForInvalidaRetornarStatus404()
        {
            //arrange
            var request = new ValidarSenhaRequest()
            {
                Senha = "AbTp9!fo"
            };

            //act
            var result = await Client.PostAsync($"Senhaaaaa/validar-senha", request);
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValidarSenhaResponse>(content);

            //assert
            result.StatusCode.Should().Be(404);
            response.Should().BeNull();
        }

        [Test]
        public async Task QuandoRequestForInvalidoDeveRetornarStatus400ESenhaValidaFalsa()
        {
            //arrange
            var request = new ValidarSenhaResponse()
            {
            };

            //act
            var result = await Client.PostAsync($"Senha/validar-senha", request);
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValidarSenhaResponse>(content);

            //assert
            result.StatusCode.Should().Be(400);
            response.Sucesso.Should().BeTrue();
            response.SenhaValida.Should().BeFalse();
        }

        [Test]
        public async Task QuandoRequestForNuloDeveRetornarStatus415()
        {
            //arrange
            
            //act
            var result = await Client.PostAsync($"Senha/validar-senha", null);
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValidarSenhaResponse>(content);

            //assert
            result.StatusCode.Should().Be(415);
            response.Sucesso.Should().BeTrue();
            response.SenhaValida.Should().Be(false);
        }
    }
}
