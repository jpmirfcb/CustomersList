using AutoMapper;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Application.UseCases.Customers.Create.Tests;

public class CreateCustomerHandlerSuccessTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public CreateCustomerHandlerSuccessTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CreateCustomerMapperProfile>());
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact()]
    public async Task CreateCustomerHandlerUnexistingClient_MustSucceed()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = "+1234567894510"
        };
        var expectedLocation = $"users/{newCustomer.Id}";

        var mockRepository = mocker.GetMock<ICustomersRepository>();
        mockRepository.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(newCustomer);

        mocker.Use(mocker.GetMock<ILogger<CreateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateCustomerHandler>();
        var request = new CreateCustomerRequest(newCustomer.Name, newCustomer.Email, newCustomer.Phone);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Created);
        result.Location.Should().EndWith(expectedLocation);
    }
}