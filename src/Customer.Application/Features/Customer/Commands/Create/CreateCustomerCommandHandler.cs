using Customer.Application.Abstractions;
using Customer.Core.Entities;
using Customer.Core.Repositories;
using Customer.Core.UoW;

namespace Customer.Application.Features.Customer.Commands.Create;

public sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CreateCustomerReponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _uow;

    public CreateCustomerCommandHandler(
        IUnitOfWork uow,
        ICustomerRepository customerRepository)
    {
        _uow = uow;
        _customerRepository = customerRepository;
    }

    public async Task<CreateCustomerReponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _customerRepository.EmailsExists(request.Email);
        if (emailExists)
            throw new ApplicationException("This email already in use");

        var model = new Core.Entities.Customer
        {
            FullName = request.FullName,
            Email = request.Email,
            Phones = request.Phones
                .Select(e => new PhoneNumber(e.AreaCode, e.Number, Enum.Parse<PhoneType>(e.Type)))
                .ToList()
        };

        await _customerRepository.AddAsync(model, cancellationToken);
        await _uow.CommitAsync(cancellationToken);
        return new CreateCustomerReponse(model.Id);
    }
}