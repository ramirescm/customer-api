using Customer.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Application;

public static class AppServiceCollectionExtension
{
    public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(IApplicationEntryPoint).Assembly)
                .AddOpenBehavior(typeof(ValidationBehavior<,>)));
        services.AddValidatorsFromAssemblies(new[] { typeof(IApplicationEntryPoint).Assembly });
    }
}