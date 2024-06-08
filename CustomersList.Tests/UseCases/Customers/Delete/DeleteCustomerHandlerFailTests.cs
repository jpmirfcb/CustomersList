using CustomersList.Application.UseCases.Customers.Delete;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Customers.Delete;

public class DeleteCustomerHandlerFailTests
{
    [Fact()]
    public async Task DeleteCustomerDetailsForEmptyCustomerId_ReturnInvalidResult()
    {
        // Arrange
        var id = string.Empty;
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<DeleteCustomerHandler>();
        var request = new DeleteCustomerRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact()]
    public async Task DeleteCustomerDetailsForNonGuidCustomerId_ReturnInvalidResult()
    {
        // Arrange
        var id = "123";
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<DeleteCustomerHandler>();
        var request = new DeleteCustomerRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact()]
    public async Task DeleteCustomerDetailsForNullCustomerId_ReturnInvalidResult()
    {
        // Arrange
        string id = null;
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<DeleteCustomerHandler>();
        var request = new DeleteCustomerRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact()]
    public async Task DeleteCustomerDetailsForEmptyGuidCustomerId_ReturnInvalidResult()
    {
        // Arrange
        var id = Guid.Empty.ToString();
        var mocker = new AutoMocker();

        var handler = mocker.CreateInstance<DeleteCustomerHandler>();
        var request = new DeleteCustomerRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.Invalid);
    }

    [Fact()]
    public async Task DeleteCustomerDetailsForNonExistingCustomerId_ReturnInvalidResult()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();

        var customer = new Customer() { Id = Guid.NewGuid(), Name = "Some Name", Email = "someone@somedomain.com", Phone = "+1234156854741" };

        var mocker = new AutoMocker();
        var repositoryMock = mocker.GetMock<ICustomersRepository>();
        repositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        var handler = mocker.CreateInstance<DeleteCustomerHandler>();
        var request = new DeleteCustomerRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.NotFound);
    }
}
