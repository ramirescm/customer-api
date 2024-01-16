using Customer.Application.Abstractions;
using Customer.Core.Repositories;
using Customer.Core.UoW;

namespace Customer.Application.Features.Customer.Queries.GetAll;

public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomerQuery, GetAllCustomerResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly ICustomerRepository _customerRepository;
    
    public GetAllCustomersQueryHandler(
        IUnitOfWork uow,
        ICustomerRepository customerRepository)
    {
        _uow = uow;
        _customerRepository = customerRepository;
    }
    
    public async Task<GetAllCustomerResponse> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAll();
        var list = customers
            .Select(c => new CustomerResponse(
                c.Id,
                c.FullName,
                c.Email,
                c.Phones.Select(e => new PhoneResponse(e.AreaCode, e.Number, e.Type.ToString())).ToList()
            ))
            .ToList();
        
        return new GetAllCustomerResponse(list);
    }
}