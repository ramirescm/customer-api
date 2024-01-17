using System.Net;
using System.Net.Http.Json;
using System.Text;
using Customer.Application.Features.Customer.Commands.Create;
using Customer.Application.Features.Customer.Commands.RemoveByEmail;
using Customer.Application.Features.Customer.Commands.UpdateEmail;
using Customer.Application.Features.Customer.Commands.UpdatePhone;
using Customer.Application.Features.Customer.Queries.GetAll;
using Customer.Application.Features.Customer.Queries.GetByPhone;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using PhoneResponse = Customer.Application.Features.Customer.Queries.GetByPhone.PhoneResponse;

namespace Customer.API.Tests;

public class EndpointTests
{
    private static readonly TestApplication _application;
    private static readonly HttpClient _client;
    private static readonly IMediator _mediator;
    
    static EndpointTests()
    {
        _application = new TestApplication();
        _client = _application.CreateClient();
        _mediator = _application.Services.GetRequiredService<IMediator>();
    }
    
    [Fact]
    public async Task CreateCustomer_ShouldReturnOk()
    {
        // Arrange 
        _mediator.Send(Arg.Any<CreateCustomerCommand>()).Returns(Task.FromResult(new CreateCustomerReponse(1)));
        
        // Act
        var createCustomerCommand = new CreateCustomerCommand
        {
            FullName = "kurt",
            Email = "kurt@kurt.com",
            Phones = new List<Phones>
            {
                new Phones { AreaCode = "123", Number = "4567890", Type = "Mobile" }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(createCustomerCommand), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("api/v1/customers", content);

        // Assert
        response.Should().NotBeNull();
        await _mediator.Received().Send(Arg.Any<CreateCustomerCommand>());
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    
    [Fact]
    public async Task GetAllCustomers_ShouldReturnOkWithCustomerList()
    {
        // Arrange
        var mockCustomerResponse1 = new CustomerResponse(1, "kurt 1", "kurt.1@aaa.com", new List<Customer.Application.Features.Customer.Queries.GetAll.PhoneResponse>());
        var mockCustomerResponse2 = new CustomerResponse(2, "Jane 2", "jane.2@bbb.com", new List<Customer.Application.Features.Customer.Queries.GetAll.PhoneResponse>());
        var mockResponse = new GetAllCustomerResponse(new List<CustomerResponse> { mockCustomerResponse1, mockCustomerResponse2 });

        _mediator.Send(Arg.Any<GetAllCustomerQuery>()).Returns(Task.FromResult(mockResponse));
        
        // Act
        var response = await _client.GetAsync("api/v1/customers");

        // Assert
        response.Should().NotBeNull();
        var customers = JsonConvert.DeserializeObject<GetAllCustomerResponse>(await response.Content.ReadAsStringAsync());
        customers.Should().NotBeNull();
        await _mediator.Received().Send(Arg.Any<GetAllCustomerQuery>());
    }
    
    [Fact]
    public async Task GetCustomerByPhone_ShouldReturnOkWithCustomer()
    {
        // Arrange
        var ddd = "123";
        var number = "4567890";
        var mockCustomerResponse = new GetCustomerByPhoneResponse(1, "kurt Doe", "kurt.doe@kurt.com", new List<PhoneResponse>());

        _mediator.Send(Arg.Any<GetCustomerByPhoneQuery>()).Returns(Task.FromResult(mockCustomerResponse));

        // Act
        var response = await _client.GetAsync($"api/v1/customers/{ddd}/{number}");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var customer = JsonConvert.DeserializeObject<GetCustomerByPhoneResponse>(content);

        customer.Should().NotBeNull();
        customer.Id.Should().Be(1); // Adjust based on your actual expected data

        await _mediator.Received().Send(Arg.Any<GetCustomerByPhoneQuery>());
    }

    
    [Fact]
    public async Task UpdateCustomerEmail_ShouldReturnOkWithUpdatedCustomer()
    {
        // Arrange
        var customerId = 1;
        var updateRequest = new UpdateCustomerEmailCommand { Email = "mary@tesla.com" };
        var mockUpdatedCustomerResponse = new UpdateCustomerEmailResponse();

        _mediator.Send(Arg.Any<UpdateCustomerEmailCommand>()).Returns(Task.FromResult(mockUpdatedCustomerResponse));

        // Act
        var response = await _client.PutAsJsonAsync($"api/v1/customers/{customerId}/email", updateRequest);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var updatedCustomer = JsonConvert.DeserializeObject<CustomerResponse>(content);

        updatedCustomer.Should().NotBeNull();
        await _mediator.Received().Send(Arg.Any<UpdateCustomerEmailCommand>());
    }

    
    [Fact]
    public async Task UpdateCustomerPhone_ShouldReturnOkWithUpdatedCustomer()
    {
        // Arrange
        var customerId = 1;
        var phoneId = 2;
        var updateRequest = new UpdateCustomerPhoneCommand
        {
            CustomerId = customerId,
            PhoneId = phoneId,
            AreaCode = "456",
            Number = "7890123",
            Type = "Mobile"
        };

        var mockUpdatedCustomerResponse = new UpdateCustomerPhoneResponse();

        _mediator.Send(Arg.Any<UpdateCustomerPhoneCommand>()).Returns(Task.FromResult(mockUpdatedCustomerResponse));

        // Act
        var response = await _client.PutAsJsonAsync($"api/v1/customers/{customerId}/phone", updateRequest);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var updatedCustomer = JsonConvert.DeserializeObject<UpdateCustomerPhoneResponse>(content);

        updatedCustomer.Should().NotBeNull();
        await _mediator.Received().Send(Arg.Any<UpdateCustomerPhoneCommand>());
    }

    [Fact]
    public async Task RemoveCustomerByEmail_ShouldReturnNoContent()
    {
        // Arrange
        var customerId = 1;
        var email = "kurt.doe@kurt.com";

        _mediator.Send(Arg.Any<RemoveCustomerByEmailCommand>()).Returns(new RemoveCustomerResponse());

        // Act
        var response = await _client.DeleteAsync($"api/v1/customers/{customerId}/email/{email}");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        await _mediator.Received().Send(Arg.Any<RemoveCustomerByEmailCommand>());
    }
}