using AutoMapper;
using CustomersList.Application.UseCases.Users.Details;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Users.Details;

public class UserDetailsHandlerSuccessTests
{
    private MapperConfiguration _mapperConfiguration;
    public UserDetailsHandlerSuccessTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<UserDetailsMapperProfile>());
    }

    [Fact()]
    public async Task GetUserDetailsForExistingUser_ReturnUser()
    {
        // Arrange
        var id = Guid.NewGuid();
        var user = new User() { Id = id, Name = "Test User", Email = "SomeEmail@gmail.com", Password = "P@55w0rd" };
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

        mocker.Use(mocker.GetMock<ILogger<UserDetailsHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UserDetailsHandler>();
        var request = new UserDetailsRequest(user.Id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Ok);
        result.Value.Should().BeOfType<UserDetailsResponse>();
        result.Value.Id.Should().NotBeEmpty().And.Be(user.Id);
        result.Value.Name.Should().NotBeEmpty().And.Be(user.Name);
        result.Value.Email.Should().NotBeEmpty().And.Be(user.Email);
    }


}
