using AutoMapper;
using CustomersList.Application.UseCases.Users.Details;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Users.Details;

public class UserDetailsHandlerFailTests
{
    private MapperConfiguration _mapperConfiguration;

    public UserDetailsHandlerFailTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<UserDetailsMapperProfile>());
    }


    [Fact()]
    public async Task GetUserDetailsForEmptyId_ReturnNull()
    {
        // Arrange
        var mocker = new AutoMocker();
        var handler = mocker.CreateInstance<UserDetailsHandler>();
        var request = new UserDetailsRequest(Guid.Empty);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }



    [Fact()]
    public async Task GetUserDetailsForNonExistingId_ReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mocker = new AutoMocker();

        var mockRepository = mocker.GetMock<IUsersRepository>();
        mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);

        mocker.Use(mocker.GetMock<ILogger<UserDetailsHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UserDetailsHandler>();
        var request = new UserDetailsRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.NotFound);
    }
}
