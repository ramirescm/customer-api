using Customer.Application.Features.Customer.Commands.Create;
using Customer.Core.Repositories;
using Customer.Core.UoW;
using NSubstitute;

namespace Customer.API.Tests.Handlers;

public class CreateCustomerCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCreateCustomerCommand_ShouldReturnCreateCustomerResponse()
    {
        // Arrange
        var uow = Substitute.For<IUnitOfWork>();
        var customerRepository = Substitute.For<ICustomerRepository>();
        var handler = new CreateCustomerCommandHandler(uow, customerRepository);

        var createCustomerCommand = new CreateCustomerCommand
        {
            FullName = "Ozzy Batman",
            Email = "ozzy@fest.com",
            Phones = new List<Phones>
            {
                new() { AreaCode = "123", Number = "4567890", Type = "Mobile" }
            }
        };

        var cancellationToken = CancellationToken.None;

        customerRepository.EmailsExists(createCustomerCommand.Email).Returns(false);

        // Act
        var result = await handler.Handle(createCustomerCommand, cancellationToken);

        // Assert
        Assert.IsType<CreateCustomerReponse>(result);
        await customerRepository.Received(1).AddAsync(Arg.Any<Core.Entities.Customer>(), cancellationToken);
        await uow.Received(1).CommitAsync(cancellationToken);
    }

    [Fact]
    public async Task Handle_DuplicateEmail_ShouldThrowApplicationException()
    {
        // Arrange
        var uow = Substitute.For<IUnitOfWork>();
        var customerRepository = Substitute.For<ICustomerRepository>();
        var handler = new CreateCustomerCommandHandler(uow, customerRepository);

        var createCustomerCommand = new CreateCustomerCommand
        {
            FullName = "Billy The Kid",
            Email = "billy@gangstar.com",
            Phones = new List<Phones>
            {
                new() { AreaCode = "123", Number = "4567890", Type = "Mobile" }
            }
        };

        var cancellationToken = CancellationToken.None;

        customerRepository.EmailsExists(createCustomerCommand.Email).Returns(true);

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(async () =>
            await handler.Handle(createCustomerCommand, cancellationToken));
    }
}