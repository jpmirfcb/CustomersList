using AutoMapper;
using CustomersList.Application.UseCases.Customers.Create;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Customers.Create;

public class CreateCustomerHandlerFailTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public CreateCustomerHandlerFailTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CreateCustomerMapperProfile>());
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact()]
    public async Task CreateCustomerHandlerIncompleteRequest_MustFail()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = string.Empty,
            Phone = "+123456789451"
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerHandlerExistingClient_MustFail()
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
        mockRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(newCustomer);

        mocker.Use(mocker.GetMock<ILogger<CreateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateCustomerHandler>();
        var request = new CreateCustomerRequest(newCustomer.Name, newCustomer.Email, newCustomer.Phone);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithANameLongerThanMaxLength_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "ThisIsAVeryLongNameThatExceedsTheMaxLengthLimitThisIsAVeryLongNameThatExceedsTheMaxLengthLimitThisIsAVeryLongNameThatExceedsTheMaxLengthLimit",
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithAnEmailLongerThanMaxLength_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = "thisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimit" +
            "thisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimitthisisaverylongemailthatexceedsthemaxlengthlimit@gmail.com",
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithPhoneNumberLongerThan15Characters_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = "+1234567894510123"
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithEmptyName_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty, //Empty Name
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithEmptyPhone_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = string.Empty //Empty phone number
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithPhoneNotMatchingPattern_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = "1234567890" // Phone number does not match the validation pattern
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithNullName_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = null, // Null Name
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithNullEmail_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = null, // Null Email
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }

    [Fact()]
    public async Task CreateCustomerWithNullPhoneNumber_MustBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newCustomer = new Customer()
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = null // Null phone number
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
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
        result.Location.Should().BeNullOrEmpty();
    }


}
 
