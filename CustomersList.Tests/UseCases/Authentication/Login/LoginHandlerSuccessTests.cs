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

public class LoginHandlerSuccessTests
{
    private MapperConfiguration _mapperConfiguration;

    public LoginHandlerSuccessTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserLoginMapperProfile>();
        });
    }

    [Fact]
    public async Task LoginExistingUser_ReturnsOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new UserLoginRequest("SomeEmail@gmail.com", "P@55w0rd!");
        var mocker = new AutoMocker();

        var repository = mocker.GetMock<IUsersRepository>();
        repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User() { Id = id, Email = request.Email, Password = request.Password });

        mocker.Use(mocker.GetMock<ILogger<CustomerDetailsHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(repository);

        var handler = mocker.CreateInstance<UserLoginHandler>();

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Ok);
        result.Value.Should().BeOfType<UserLoginResponse>();
        result.Value.Email.Should().NotBeEmpty().And.Be(request.Email);
    }

}
