using Itau.Senha.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Itau.Senha.Integrations.Tests
{
    public sealed class HostWebApi : IAsyncDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public HttpClient Client { get; private set; }
        public IConfiguration Configuration { get; private set; }

        private readonly string urlApi = "https://localhost:5001/api/v1/";
        private readonly WebApplicationFactory<Startup> factory;
        private readonly IServiceScope serviceScope;

        public HostWebApi()
        {
            factory = new WebApplicationFactory<Startup>();
            factory = factory.WithWebHostBuilder(builder =>
                builder.UseEnvironment(WebApiSetupFixture.TestEnvironment)
                .ConfigureTestServices(services =>
                {
                    using var sp = services.BuildServiceProvider();
                    var configuration = sp.GetService<IConfiguration>();
                }).ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Test.Integration.json"));
                    configuration.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Test.Integration.User.json"), optional: true);
                    configuration.AddEnvironmentVariables();
                }));

            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(urlApi)
            });

            serviceScope = factory.Services.CreateScope();
            ServiceProvider = serviceScope.ServiceProvider;
            Configuration = ServiceProvider.GetService<IConfiguration>();
        }

        public async ValueTask DisposeAsync()
        {
            Client.Dispose();
            if (serviceScope is IAsyncDisposable asyncDisposableScope)
                await asyncDisposableScope.DisposeAsync();
            else
                serviceScope.Dispose();
            factory.Dispose();
        }
    }
}
