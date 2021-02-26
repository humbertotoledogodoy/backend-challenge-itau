using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Itau.Senha.Integrations.Tests
{
    public abstract class BaseTest
    {
        public IServiceScope Scope { get; private set; } = default!;
        public IServiceProvider ServiceProvider { get; private set; } = default!;
        public HttpClient Client { get; private set; } = WebApiSetupFixture.Host.Client;

      
        [OneTimeSetUp]
        public virtual void OneTimeSetUpAsync()
        {
            Scope = WebApiSetupFixture.Host.ServiceProvider.CreateScope();
            ServiceProvider = Scope.ServiceProvider;
            Client = WebApiSetupFixture.Host.Client;
        }

        [OneTimeTearDown]
        public virtual async Task OneTimeTearDownAsync()
        {

            if (Scope is IAsyncDisposable asyncDisposableScope)
                await asyncDisposableScope.DisposeAsync();
            else
                Scope.Dispose();
        }

      
    }
}
