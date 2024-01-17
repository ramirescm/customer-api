using Customer.Application.Abstractions;

namespace Customer.Application.Features.Customer.Commands.UpdateEmail;

public class UpdateCustomerEmailCommand : ICommand<UpdateCustomerEmailResponse>
{
    public int CustomerId { get; set; }
    public string Email { get; set; }
}

public record UpdateCustomerEmailResponse;