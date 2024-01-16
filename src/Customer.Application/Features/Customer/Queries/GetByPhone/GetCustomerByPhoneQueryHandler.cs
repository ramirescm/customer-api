using Customer.Application.Abstractions;
using Customer.Application.Commands.Customer.GetByPhone;
using Customer.Core.Repositories;
using Customer.Core.UoW;

namespace Customer.Application.Features.Customer.Queries.GetByPhone;

public class GetCustomerByPhoneQueryHandler : IQueryHandler<GetCustomerByPhoneQuery, GetCustomerByPhoneResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly ICustomerRepository _customerRepository;
    
    public GetCustomerByPhoneQueryHandler(
        IUnitOfWork uow,
        ICustomerRepository customerRepository)
    {
        _uow = uow;
        _customerRepository = customerRepository;
    }
    
    public async Task<GetCustomerByPhoneResponse> Handle(GetCustomerByPhoneQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByPhone(request.areaCode, request.number);
        var phones = customer.Phones.Select(e => new Phone(e.AreaCode, e.Number, e.Type.ToString())).ToList();
        return new GetCustomerByPhoneResponse(customer.Id, customer.FullName, customer.Email, phones);
    }
}