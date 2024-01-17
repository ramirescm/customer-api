using Customer.Application.Abstractions;

namespace Customer.Application.Features.Customer.Queries.GetAll;

public class GetAllCustomerQuery : IQuery<GetAllCustomerResponse>
{
}

public record GetAllCustomerResponse(List<CustomerResponse> Customers);

public record CustomerResponse(int Id, string FullName, string Email, List<PhoneResponse> Phones);

public record PhoneResponse(string AreaCode, string Number, string Type);