using Customer.API;
using Customer.API.Middlewares;
using Customer.Application;
using Customer.Core.Repositories;
using Customer.Core.UoW;
using Customer.Infra;
using Customer.Infra.Repositories;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);

var app = builder.Build();
ConfigureApp(app);

app.Run();

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;
    var configuration = builder.Configuration;

    services.AddRouting(options => options.LowercaseUrls = true);
    services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    {
        options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

    services.AddEndpointsApiExplorer();
    services.AddTransient<ExceptionHandlingMiddleware>();
    services.AddValidatorsFromAssemblyContaining<IApplicationEntryPoint>();
    services.AddFluentValidationRulesToSwagger();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo()
        {
            Title = "Customer.API",
            Version = "v1",
            Description = "Customer Crud"
        });
    });

    services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("Customers"));

    // TODO: if you want save data in postgresql
    // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    // services.AddDbContext<CustomerContext>(opt =>
    //     opt.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseSettings") ?? throw new ArgumentException("DatabaseSettings not found")));

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<ICustomerRepository, CustomerRepository>();
    services.ConfigureAppDependencies(configuration);
}

static void ConfigureApp(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.DocumentTitle = "AA";
        s.DisplayRequestDuration();
        s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        s.EnableDeepLinking();
        s.ShowExtensions();
        s.ShowCommonExtensions();
    });
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.MapEndpoints();
}