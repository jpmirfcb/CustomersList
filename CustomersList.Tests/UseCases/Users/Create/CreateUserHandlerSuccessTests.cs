using AutoMapper;
using CustomersList.Application.UseCases.Users.CreateUser;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Users.Create;

public class CreateUserHandlerSuccessTests
{
    private MapperConfiguration _mapperConfiguration;
    public CreateUserHandlerSuccessTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CreateUserMapperProfile>());
    }

    [Fact]
    public async Task CreateUserWithProperParameters_ShouldReturnCreated()
    {
        // Arrange
        var mocker = new AutoMocker();

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "New User",
            Email = "someemail@gmail.com",
            Password = "P@55w0rd!"
        };
        var expectedLocation = $"users/{newUser.Id}";

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mockRepository.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(newUser);

        mocker.Use(mocker.GetMock<ILogger<CreateUserHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<CreateUserHandler>();
        var request = new CreateUserRequest(newUser.Email, newUser.Password, newUser.Name);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Created);
        result.Location.Should().EndWith(expectedLocation);
    }

}
