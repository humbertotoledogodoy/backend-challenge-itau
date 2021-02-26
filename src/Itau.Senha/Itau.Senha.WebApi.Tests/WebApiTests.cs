using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Itau.Senha.WebApi.Tests
{
    public class WebApiTests
    {
        private Startup startup = default!;

        [SetUp]
        public void Init() => startup = new Startup();

        [Test]
        public void DeveTestarStartup()
        {
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
        }



    }
}
