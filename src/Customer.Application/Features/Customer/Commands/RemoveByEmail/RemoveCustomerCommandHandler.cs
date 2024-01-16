using Customer.Application.Abstractions;
using Customer.Core.Repositories;
using Customer.Core.UoW;

namespace Customer.Application.Features.Customer.Commands.RemoveByEmail;

public class RemoveCustomerCommandHandler : ICommandHandler<RemoveCustomerByEmailCommand, RemoveCustomerResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly ICustomerRepository _customerRepository;
    
    public RemoveCustomerCommandHandler(
        IUnitOfWork uow,
        ICustomerRepository customerRepository)
    {
        _uow = uow;
        _customerRepository = customerRepository;
    }
    
    public async Task<RemoveCustomerResponse> Handle(RemoveCustomerByEmailCommand request, CancellationToken cancellationToken)
    {
        await _customerRepository.RemoveByEmail(request.CustomerId, request.Email);
        await _uow.CommitAsync(cancellationToken);
        return new RemoveCustomerResponse();
    }
}