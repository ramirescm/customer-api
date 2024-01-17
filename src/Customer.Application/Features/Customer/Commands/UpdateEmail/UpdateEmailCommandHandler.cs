using Customer.Application.Abstractions;
using Customer.Core.Repositories;
using Customer.Core.UoW;

namespace Customer.Application.Features.Customer.Commands.UpdateEmail;

public class UpdateEmailCommandHandler : ICommandHandler<UpdateCustomerEmailCommand, UpdateCustomerEmailResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _uow;

    public UpdateEmailCommandHandler(
        IUnitOfWork uow,
        ICustomerRepository customerRepository)
    {
        _uow = uow;
        _customerRepository = customerRepository;
    }

    public async Task<UpdateCustomerEmailResponse> Handle(UpdateCustomerEmailCommand request,
        CancellationToken cancellationToken)
    {
        await _customerRepository.UpdateEmail(request.CustomerId, request.Email);
        await _uow.CommitAsync(cancellationToken);
        return new UpdateCustomerEmailResponse();
    }
}