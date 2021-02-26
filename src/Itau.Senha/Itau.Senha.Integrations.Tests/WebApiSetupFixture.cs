using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Itau.Senha.Integrations.Tests
{
    [SetUpFixture]
    [Category("IntegrationTest")]
    public class WebApiSetupFixture
    {
        public const string TestEnvironment = "Test";
        public static HostWebApi Host { get; private set; } = default!;

        [OneTimeSetUp]
        public virtual void OneTimeSetUpAsync()
        {
            AssertionOptions.AssertEquivalencyUsing(options =>
            {
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1000)).WhenTypeIs<DateTime>();
                options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1000)).WhenTypeIs<DateTimeOffset>();
                return options;
            });

            Host = new HostWebApi();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await Host.DisposeAsync();
        }
    }
}
