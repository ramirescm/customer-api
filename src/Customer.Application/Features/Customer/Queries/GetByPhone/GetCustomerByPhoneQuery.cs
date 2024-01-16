using Customer.Application.Abstractions;

namespace Customer.Application.Commands.Customer.GetByPhone;

public record GetCustomerByPhoneQuery(string areaCode, string number) : IQuery<GetCustomerByPhoneResponse>;

public record GetCustomerByPhoneResponse(int id, string fullName, string email, List<Phone> phones);

public record Phone(string areaCode, string number, string type);
