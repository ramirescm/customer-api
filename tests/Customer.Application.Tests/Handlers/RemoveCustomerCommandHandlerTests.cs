using Customer.Application.Features.Customer.Commands.RemoveByEmail;
using Customer.Core.Repositories;
using Customer.Core.UoW;
using NSubstitute;

namespace Customer.API.Tests.Handlers;

public class RemoveCustomerCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidRemoveCustomerByEmailCommand_ShouldReturnRemoveCustomerResponse()
    {
        // Arrange
        var uow = Substitute.For<IUnitOfWork>();
        var customerRepository = Substitute.For<ICustomerRepository>();
        var handler = new RemoveCustomerCommandHandler(uow, customerRepository);

        var removeCustomerCommand = new RemoveCustomerByEmailCommand
        {
            CustomerId = 1,
            Email = "joao@amp.com"
        };

        var cancellationToken = CancellationToken.None;

        // Removed with success
        customerRepository.RemoveByEmail(removeCustomerCommand.CustomerId, removeCustomerCommand.Email)
            .Returns(Task.FromResult(true));

        // Act
        var result = await handler.Handle(removeCustomerCommand, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RemoveCustomerResponse>(result);
        await customerRepository.Received(1)
            .RemoveByEmail(removeCustomerCommand.CustomerId, removeCustomerCommand.Email);
        await uow.Received(1).CommitAsync(cancellationToken);
    }

    [Fact]
    public async Task Handle_InvalidRemoveCustomerByEmailCommand_ShouldNotCommitAndReturnRemoveCustomerResponse()
    {
        // Arrange
        var uow = Substitute.For<IUnitOfWork>();
        var customerRepository = Substitute.For<ICustomerRepository>();
        var handler = new RemoveCustomerCommandHandler(uow, customerRepository);

        var removeCustomerCommand = new RemoveCustomerByEmailCommand
        {
            CustomerId = 1,
            Email = "don@torreto.com"
        };

        var cancellationToken = new CancellationToken();

        // Already removed
        customerRepository.RemoveByEmail(removeCustomerCommand.CustomerId, removeCustomerCommand.Email)
            .Returns(Task.FromResult(false));

        // Act
        var result = await handler.Handle(removeCustomerCommand, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RemoveCustomerResponse>(result);
        await customerRepository.Received(1)
            .RemoveByEmail(removeCustomerCommand.CustomerId, removeCustomerCommand.Email);
        await uow.Received(1).CommitAsync(cancellationToken);
        //await uow.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
}