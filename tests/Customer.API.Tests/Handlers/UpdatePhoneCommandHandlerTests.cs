using Customer.Application.Features.Customer.Commands.UpdatePhone;
using Customer.Core.Repositories;
using Customer.Core.UoW;
using NSubstitute;

namespace Customer.API.Tests.Handlers;

public class UpdatePhoneCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidUpdateCustomerPhoneCommand_ShouldCommitAndReturnUpdateCustomerPhoneResponse()
    {
        // Arrange
        var uow = Substitute.For<IUnitOfWork>();
        var customerRepository = Substitute.For<ICustomerRepository>();
        var handler = new UpdatePhoneCommandHandler(uow, customerRepository);

        var updateCustomerPhoneCommand = new UpdateCustomerPhoneCommand
        {
            CustomerId = 1,
            PhoneId = 123,
            AreaCode = "456",
            Number = "7890123",
            Type = "Mobile"
        };

        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(updateCustomerPhoneCommand, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UpdateCustomerPhoneResponse>(result);
        await customerRepository.Received(1).UpdatePhone(
            updateCustomerPhoneCommand.CustomerId,
            updateCustomerPhoneCommand.PhoneId,
            updateCustomerPhoneCommand.AreaCode,
            updateCustomerPhoneCommand.Number,
            updateCustomerPhoneCommand.Type
        );
        await uow.Received(1).CommitAsync(cancellationToken);
    }
}