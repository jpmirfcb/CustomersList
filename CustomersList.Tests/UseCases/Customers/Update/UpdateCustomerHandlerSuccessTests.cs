using AutoMapper;
using CustomersList.Application.UseCases.Customers.Update;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Customers.Update;

public class UpdateCustomerHandlerSuccessTests
{
    private readonly MapperConfiguration _mapperConfiguration;

    public UpdateCustomerHandlerSuccessTests()
    {
        _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<UpdateCustomerMapperProfile>());
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact()]
    public async Task UpdateCustomerHandlerWithProperParaameters_MustReturnNoContent()
    {
        // Arrange
        var mocker = new AutoMocker();

        var id = Guid.NewGuid();

        var mockRepository = mocker.GetMock<ICustomersRepository>();
        mockRepository.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        mocker.Use(mocker.GetMock<ILogger<UpdateCustomerHandler>>());
        mocker.Use(_mapperConfiguration.CreateMapper());
        mocker.Use(mockRepository);

        var handler = mocker.CreateInstance<UpdateCustomerHandler>();
        var request = new UpdateCustomerRequest()
        {
            Id = id.ToString(),
            Name = "New Customer",
            Email = "someemail@gmail.com",
            Phone = "+123456789451"
        };

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.NoContent);
    }


}
