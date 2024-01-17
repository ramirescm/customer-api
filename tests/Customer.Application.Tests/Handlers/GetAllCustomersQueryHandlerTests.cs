using Customer.Application.Features.Customer.Queries.GetAll;
using Customer.Core.Entities;
using Customer.Core.Repositories;
using Customer.Core.UoW;
using FluentAssertions;
using NSubstitute;

namespace Customer.Application.Tests.Handlers;

public class GetAllCustomersQueryHandlerTests
{
    [Fact]
    public async Task Handle_ValidGetAllCustomerQuery_ShouldReturnAllCustomers()
    {
        // Arrange
        var uow = Substitute.For<IUnitOfWork>();
        var customerRepository = Substitute.For<ICustomerRepository>();
        var handler = new GetAllCustomersQueryHandler(uow, customerRepository);

        var getAllCustomerQuery = new GetAllCustomerQuery();
        var cancellationToken = CancellationToken.None;

        var customersFromRepository = new List<Core.Entities.Customer>
        {
            new()
            {
                Id = 1, FullName = "Maria", Email = "maria@gmail.com",
                Phones = new List<PhoneNumber> { new("56", "111", PhoneType.Mobile) }
            },
            new()
            {
                Id = 2, FullName = "Jane", Email = "jane@hotmail.com",
                Phones = new List<PhoneNumber> { new("33", "333", PhoneType.Mobile) }
            }
        };

        customerRepository.GetAll().Returns(await Task.FromResult(customersFromRepository));

        // Act
        var result = await handler.Handle(getAllCustomerQuery, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<GetAllCustomerResponse>(result);

        var expectedResponse = customersFromRepository
            .Select(c => new CustomerResponse(
                c.Id,
                c.FullName,
                c.Email,
                c.Phones.Select(e => new PhoneResponse(e.AreaCode, e.Number, e.Type.ToString())).ToList()
            ))
            .ToList();

        result.Customers.Should().BeEquivalentTo(expectedResponse);
    }
}