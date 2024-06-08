using AutoMapper;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Application.UseCases.Customers.Details.Tests;

public class CustomerDetailsHandlerFailTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public CustomerDetailsHandlerFailTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CustomerDetailsMapperProfile>());
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact()]
    public async Task GetCustomerDetailsForEmptyCustomerId_ReturnInvalidResult()
    {
        // Arrange
        var id = string.Empty;
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<CustomerDetailsHandler>();
        var request = new CustomerDetailsRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact()]
    public async Task GetCustomerDetailsForNullCustomerId_ReturnInvalidResult()
    {
        // Arrange
        string id = null;
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<CustomerDetailsHandler>();
        var request = new CustomerDetailsRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact()]
    public async Task GetCustomerDetailsForEmptyGuidCustomerId_ReturnInvalidResult()
    {
        // Arrange
        var id = Guid.Empty;
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<CustomerDetailsHandler>();
        var request = new CustomerDetailsRequest(id.ToString());

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact()]
    public async Task GetCustomerDetailsForNonGuidCustomerId_ReturnInvalidResult()
    {
        // Arrange
        var id = "12345";
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<CustomerDetailsHandler>();
        var request = new CustomerDetailsRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact()]
    public async Task GetCustomerDetailsForNonExistingCustomerId_ReturnNotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var mocker = new AutoMocker();
        Customer customer = null;

        // Mocking the repository
        var mockRepository = mocker.GetMock<ICustomersRepository>();
        mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customer);

        var handler = mocker.CreateInstance<CustomerDetailsHandler>();
        var request = new CustomerDetailsRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.NotFound);
    }
}
