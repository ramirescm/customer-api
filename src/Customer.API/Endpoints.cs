using Customer.Application.Commands.Customer.GetByPhone;
using Customer.Application.Features.Customer.Commands.Create;
using Customer.Application.Features.Customer.Commands.RemoveByEmail;
using Customer.Application.Features.Customer.Commands.UpdateEmail;
using Customer.Application.Features.Customer.Commands.UpdatePhone;
using Customer.Application.Features.Customer.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API;

public static class Endpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/customers", async (IMediator mediator, CreateCustomerCommand request)
            => await mediator.Send(request));

        app.MapGet("api/v1/customers", async (IMediator mediator) =>
        {
            var customer = await mediator.Send(new GetAllCustomerQuery());
            return Results.Ok(customer);
        });

        app.MapGet("api/v1/customers/{ddd}/{number}", async (IMediator mediator, string ddd, string number) =>
        {
            var customer = await mediator.Send(new GetCustomerByPhoneQuery(ddd, number));
            return Results.Ok(customer);
        });

        app.MapPut("api/v1/customers/{id}/email",
            async (IMediator mediator, int id, [FromBody] UpdateCustomerEmailCommand request) =>
            {
                request.CustomerId = id;
                var customer = await mediator.Send(request);
                return Results.Ok(customer);
            });

        app.MapPut("api/v1/customers/{id}/phone",
            async (IMediator mediator, int id, [FromBody] UpdateCustomerPhoneCommand request) =>
            {
                request.CustomerId = id;
                var customer = await mediator.Send(request);
                return Results.Ok(customer);
            });

        app.MapDelete("api/v1/customers/{id}/email",
            async (IMediator mediator, int id, [FromBody] RemoveCustomerByEmailCommand request) =>
            {
                request.CustomerId = id;
                await mediator.Send(request);
                return Results.NoContent();
            });
    }
}