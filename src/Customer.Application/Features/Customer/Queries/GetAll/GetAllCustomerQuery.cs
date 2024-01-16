using Customer.Application.Abstractions;

namespace Customer.Application.Features.Customer.Queries.GetAll;

public class GetAllCustomerQuery : IQuery<GetAllCustomerResponse>
{
}

public record GetAllCustomerResponse(List<CustomerResponse> Customers);

public record CustomerResponse(int id, string fullName, string email, List<PhoneResponse> phones);
public record PhoneResponse(string areaCode, string number, string type);