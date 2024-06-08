using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Customers.List;
using CustomersList.Application.UseCases.Customers.Update;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Customers.List;

public class CustomersListHandlerFailTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public CustomersListHandlerFailTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomersListMapperProfile>();
        });
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnEmptyResponse_WhenThereIsNoCustomers()
    {
        // Arrange
        var mocker = new AutoMocker();
        var mockRepository = mocker.GetMock<ICustomersRepository>();
        mockRepository.Setup(x => x.GetListAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((new List<Customer>(), 0));

        mocker.Use(mocker.GetMock<ILogger<CustomersListHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CustomersListHandler>();
        var request = new CustomersListRequest(1, 10);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().NotBeNull();
        result.Value.Customers.Should().NotBeNull();
        result.Value.Customers.Should().BeEmpty();
        result.Value.TotalRecords.Should().Be(0);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnInvalidResponse_WhenPageSizeIsDifferentThan10_20Or50()
    {
        // Arrange
        var mocker = new AutoMocker();
        var mockRepository = mocker.GetMock<ICustomersRepository>();

        var handler = mocker.CreateInstance<CustomersListHandler>();
        var request = new CustomersListRequest(1, 15); // Change the page size to 15

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnInvalidResponse_WhenPageSizeIsLessThan1()
    {
        // Arrange
        var mocker = new AutoMocker();
        var mockRepository = mocker.GetMock<ICustomersRepository>();

        var handler = mocker.CreateInstance<CustomersListHandler>();
        var request = new CustomersListRequest(1, 0); // Change the page size to 0

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Invalid);
    }
}
