using Customer.Application.Abstractions;

namespace Customer.Application.Features.Customer.Queries.GetByPhone;

public record GetCustomerByPhoneQuery(string AreaCode, string Number) : IQuery<GetCustomerByPhoneResponse>;

public record GetCustomerByPhoneResponse(int Id, string FullName, string Email, List<PhoneResponse> Phones);

public record PhoneResponse(string AreaCode, string Number, string Type);