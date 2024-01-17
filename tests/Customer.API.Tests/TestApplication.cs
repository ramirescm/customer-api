using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute;

namespace TestProject1;

internal class TestApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;
    public TestApplication(string environment = "Development") => _environment = environment;
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var mediator = Substitute.For<IMediator>();
        builder.ConfigureServices(services =>
        {
            services.AddTransient(_ => mediator);
        });

        builder.UseEnvironment(_environment);
        return base.CreateHost(builder);
    }
}