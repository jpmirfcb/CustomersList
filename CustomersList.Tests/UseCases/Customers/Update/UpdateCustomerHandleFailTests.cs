using AutoMapper;
using CustomersList.Application.UseCases.Customers.Create;
using CustomersList.Application.UseCases.Customers.Update;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Customers.Update;

public class UpdateCustomerHandleFailTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public UpdateCustomerHandleFailTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<UpdateCustomerMapperProfile>());
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithUnexistingId_MustReturnNotFound()
    {
        // Arrange
        var mocker = new AutoMocker();

        var customer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = "+123456789451"
        };
        var expectedLocation = $"users/{customer.Id}";

        var mockRepository = mocker.GetMock<ICustomersRepository>();
        mockRepository.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = customer.Id.ToString(),
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.NotFound);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithNullId_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = null,
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithEmptyId_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = string.Empty,
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithEmptyGuidId_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = Guid.Empty.ToString(),
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithNullName_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = Guid.NewGuid().ToString(),
            Name = null,
            Email = "someemail@gmail.com",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithEmptyName_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = Guid.NewGuid().ToString(),
            Name = string.Empty,
            Email = "someemail@gmail.com",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithNameTooLong_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "This is a very long name that exceeds the maximum allowed length of 100 characters and it's even longer than that",
            Email = "someemail@gmail.com",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithNullEmail_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "New Customer",
            Email = null,
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithEmptyEmail_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "New Customer",
            Email = string.Empty,
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithEmailTooLong_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "New Customer",
            Email = "thisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimit@gmail.com",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithInvalidEmail_MustReturnInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<ICustomersRepository>();

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "New Customer",
            Email = "invalidemail",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }
}
