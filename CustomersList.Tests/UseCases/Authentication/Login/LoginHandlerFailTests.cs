using AutoMapper;
using CustomersList.Application.UseCases.Authentication.Login;
using CustomersList.Application.UseCases.Customers.Details;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Authentication.Login;

public class LoginHandlerFailTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public LoginHandlerFailTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CustomerDetailsMapperProfile>());
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact]
    public async Task LoginUnexistingUser_ReturnsInvalid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new UserLoginRequest("SomeEmail@gmail.com", "P@55w0rd!");
        var mocker = new AutoMocker();

        var repository = mocker.GetMock<IUsersRepository>();
        repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

        mocker.Use(mocker.GetMock<ILogger<CustomerDetailsHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(repository);

        var handler = mocker.CreateInstance<UserLoginHandler>();

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task LoginWithEmptyEmail_ReturnsInvalid()
    {
        // Arrange
        var request = new UserLoginRequest("", "P@55w0rd!");
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<UserLoginHandler>();

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task LoginWithLongEmail_ReturnsInvalid()
    {
        // Arrange
        var request = new UserLoginRequest("SomeVeryLongEmailThatExceedsTheMaximumLengthOf255CharactersAndShouldCauseAnError@gmail.com".PadLeft(256, 'a'), "P@55w0rd!");
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<UserLoginHandler>();

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task LoginWithEmptyPassword_ReturnsInvalid()
    {
        // Arrange
        var request = new UserLoginRequest("SomeEmail@gmail.com", "");
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<UserLoginHandler>();

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact]
    public async Task LoginWithShortPassword_ReturnsInvalid()
    {
        // Arrange
        var request = new UserLoginRequest("SomeEmail@gmail.com", "12345");
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<UserLoginHandler>();

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }
}
