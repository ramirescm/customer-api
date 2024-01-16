using Customer.Application.Abstractions;
using Customer.Core.Repositories;
using Customer.Core.UoW;

namespace Customer.Application.Features.Customer.Commands.UpdatePhone;

public class UpdatePhoneCommandHandler : ICommandHandler<UpdateCustomerPhoneCommand, UpdateCustomerPhoneResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly ICustomerRepository _customerRepository;

    public UpdatePhoneCommandHandler(
        IUnitOfWork uow,
        ICustomerRepository customerRepository)
    {
        _uow = uow;
        _customerRepository = customerRepository;
    }

    public async Task<UpdateCustomerPhoneResponse> Handle(UpdateCustomerPhoneCommand request,
        CancellationToken cancellationToken)
    {
        await _customerRepository.UpdatePhone(request.CustomerId, request.PhoneId, request.AreaCode, request.Number,
            request.Type);
        await _uow.CommitAsync(cancellationToken);
        return new UpdateCustomerPhoneResponse();
    }
}