using Customer.Application.Abstractions;

namespace Customer.Application.Features.Customer.Commands.UpdatePhone;

public class UpdateCustomerPhoneCommand : ICommand<UpdateCustomerPhoneResponse>
{
    public int CustomerId { get; set; }

    public int PhoneId { get; set; }
    public string AreaCode { get; set; }

    public string Number { get; set; }

    public string Type { get; set; }
}

public record UpdateCustomerPhoneResponse;