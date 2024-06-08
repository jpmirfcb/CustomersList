using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Customers.List;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Customers.List;

public class CustomersListHandlerSuccessTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    private List<Customer> _customersList;

    public CustomersListHandlerSuccessTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomersListMapperProfile>();
        });

        _customersList = new List<Customer>()
        {
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 1", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 2", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 3", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 4", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 5", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 6", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 7", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 8", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 9", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 10", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 11", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 12", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 13", Email = "someEmail@domain.com", Phone = "+2135454132154" },
            new Customer() { Id = Guid.NewGuid(), Name = "Customer 14", Email = "someEmail@domain.com", Phone = "+2135454132154" }
        };
    }

    [Fact]
    public async Task CustomerListHandlerWithProperParmeters_ShouldReturnFirstPage()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var mocker = new AutoMocker();
        var mockRepository = mocker.GetMock<ICustomersRepository>();
        mockRepository.Setup(x => x.GetListAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((_customersList.Skip((pageNumber-1) * pageSize).Take(pageSize), _customersList.Count ));

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
        result.Value.Customers.Should().NotBeEmpty();
        result.Value.TotalRecords.Should().Be(_customersList.Count);
    }
    [Fact]
    public async Task CustomerListHandlerWithProperParmeters_ShouldReturnLastPage()
    {
        // Arrange
        var pageNumber = 2;
        var pageSize = 10;
        var mocker = new AutoMocker();
        var mockRepository = mocker.GetMock<ICustomersRepository>();
        mockRepository.Setup(x => x.GetListAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((_customersList.Skip((pageNumber - 1) * pageSize).Take(pageSize), _customersList.Count));

        mocker.Use(mocker.GetMock<ILogger<CustomersListHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CustomersListHandler>();
        var request = new CustomersListRequest(2, 10);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().NotBeNull();
        result.Value.Customers.Should().NotBeNull();
        result.Value.Customers.Should().NotBeEmpty();
        result.Value.Customers.Count().Should().BeLessOrEqualTo(pageSize);
        result.Value.TotalRecords.Should().Be(_customersList.Count);
    }


}

