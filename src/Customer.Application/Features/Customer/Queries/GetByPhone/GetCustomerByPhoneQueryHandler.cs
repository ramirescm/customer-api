using Customer.Application.Abstractions;
using Customer.Core.Repositories;
using Customer.Core.UoW;

namespace Customer.Application.Features.Customer.Queries.GetByPhone;

public class GetCustomerByPhoneQueryHandler : IQueryHandler<GetCustomerByPhoneQuery, GetCustomerByPhoneResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _uow;

    public GetCustomerByPhoneQueryHandler(
        IUnitOfWork uow,
        ICustomerRepository customerRepository)
    {
        _uow = uow;
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomerByPhoneResponse> Handle(GetCustomerByPhoneQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByPhone(request.AreaCode, request.Number);
        var phones = customer.Phones.Select(e => new PhoneResponse(e.AreaCode, e.Number, e.Type.ToString())).ToList();
        return new GetCustomerByPhoneResponse(customer.Id, customer.FullName, customer.Email, phones);
    }
}