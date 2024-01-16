using Customer.Application.Abstractions;

namespace Customer.Application.Features.Customer.Commands.RemoveByEmail;

public class RemoveCustomerByEmailCommand : ICommand<RemoveCustomerResponse>
{
    public int CustomerId { get; set; }
    public string Email { get; set; }
}

public record RemoveCustomerResponse();