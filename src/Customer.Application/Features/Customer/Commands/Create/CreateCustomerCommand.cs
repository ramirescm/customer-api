using Customer.Application.Abstractions;

namespace Customer.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommand : ICommand<CreateCustomerReponse>
{
    public CreateCustomerCommand()
    {
    }

    public CreateCustomerCommand(string fullName, string email, List<Phones> phones)
    {
        FullName = fullName;
        Email = email;
        Phones = phones;
    }

    public string FullName { get; set; }
    public string Email { get; set; }
    public List<Phones> Phones { get; set; }
}

public class Phones
{
    public Phones()
    {
    }

    public Phones(string areaCode, string number, string type)
    {
        AreaCode = areaCode;
        Number = number;
        Type = type;
    }

    public string AreaCode { get; set; }
    public string Number { get; set; }
    public string Type { get; set; }
}

public record CreateCustomerReponse(int Id);