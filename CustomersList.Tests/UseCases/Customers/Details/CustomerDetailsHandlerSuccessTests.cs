using AutoMapper;
using CustomersList.Application.UseCases.Customers.Details;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Customers.Details;

public class CustomerDetailsHandlerSuccessTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public CustomerDetailsHandlerSuccessTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CustomerDetailsMapperProfile>());
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact()]
    public async Task GetCustomerDetailsForExistingCustomer_ReturnCustomer()
    {

        // Arrange
        var id = Guid.NewGuid();
        var customer = new Customer() { Id = id, Name = "Test Customer", Email = "SomeEmail@gmail.com", Phone = "+1234567891245" };
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();
        mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customer);

        mocker.Use(mocker.GetMock<ILogger<CustomerDetailsHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);


        var handler = mocker.CreateInstance<CustomerDetailsHandler>();
        var request = new CustomerDetailsRequest(customer.Id.ToString());

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Ok);
        result.Value.Should().BeOfType<CustomerDetailsResponse>();
        result.Value.Id.Should().NotBeEmpty().And.Be(customer.Id);
        result.Value.Name.Should().NotBeEmpty().And.Be(customer.Name);
        result.Value.Email.Should().NotBeEmpty().And.Be(customer.Email);
        result.Value.Phone.Should().NotBeEmpty().And.Be(customer.Phone);
    }
}
