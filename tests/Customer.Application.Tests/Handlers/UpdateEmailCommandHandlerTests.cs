using Customer.Application.Features.Customer.Commands.UpdateEmail;
using Customer.Core.Repositories;
using Customer.Core.UoW;
using NSubstitute;

namespace Customer.API.Tests.Handlers;

public class UpdateEmailCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidUpdateCustomerEmailCommand_ShouldCommitAndReturnUpdateCustomerEmailResponse()
    {
        // Arrange
        var uow = Substitute.For<IUnitOfWork>();
        var customerRepository = Substitute.For<ICustomerRepository>();
        var handler = new UpdateEmailCommandHandler(uow, customerRepository);

        var updateCustomerEmailCommand = new UpdateCustomerEmailCommand
        {
            CustomerId = 1,
            Email = "maxtrix@revolutions.com"
        };

        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(updateCustomerEmailCommand, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UpdateCustomerEmailResponse>(result);
        await customerRepository.Received(1)
            .UpdateEmail(updateCustomerEmailCommand.CustomerId, updateCustomerEmailCommand.Email);
        await uow.Received(1).CommitAsync(cancellationToken);
    }
}