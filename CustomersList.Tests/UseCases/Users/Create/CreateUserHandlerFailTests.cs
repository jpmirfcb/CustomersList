using AutoMapper;
using CustomersList.Application.UseCases.Users.CreateUser;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Users.Create;

public class CreateUserHandlerFailTests
{
    private MapperConfiguration _mapperConfiguration;
    public CreateUserHandlerFailTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CreateUserMapperProfile>());
    }

    [Fact]
    public async Task CreateUserWithEmptyEmail_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "New User",
            Email = "",
            Password = "P@55w0rd!"
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task CreateUserWithNullEmail_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "New User",
            Email = null,
            Password = "P@55w0rd!"
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task CreateUserWithEmptyName_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "",
            Email = "someemail@gmail.com",
            Password = "P@55w0rd!"
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task CreateUserWithNullName_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = null,
            Email = "someemail@gmail.com",
            Password = "P@55w0rd!"
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task CreateUserWithEmptyPassword_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "New User",
            Email = "someemail@gmail.com",
            Password = ""
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task CreateUserWithNullPassword_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "New User",
            Email = "someemail@gmail.com",
            Password = null
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task CreateUserWithNameLongerThan100Characters_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "This is a very long name that exceeds the maximum allowed length of 100 characters and it's even longer than that",
            Email = "someemail@gmail.com",
            Password = "P@55w0rd!"
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task CreateUserWithEmailLongerThan255Characters_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "New User",
            Email = "someemail@gmail.com".PadLeft(256, 'a'),
            Password = "P@55w0rd!"
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task CreateUserWithPasswordShorterThan6Characters_ShouldBeInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "New User",
            Email = "someemail@gmail.com",
            Password = "P@55w"
        };

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }
}
