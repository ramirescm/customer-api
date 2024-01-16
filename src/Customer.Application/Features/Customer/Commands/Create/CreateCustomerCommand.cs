using Customer.Application.Abstractions;

namespace Customer.Application.Features.Customer.Commands.Create;

public record CreateCustomerCommand(
    string FullName,
    string Email,
    List<Phones> Phones) : ICommand<CreateCustomerReponse>;

public record CreateCustomerReponse(int Id);

public record Phones(string AreaCode, string Number, string Type);
