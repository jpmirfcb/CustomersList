using CustomersList.Application.UseCases.Customers.Delete;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace CustomersList.Tests.UseCases.Customers.Delete;

public class DeleteCustomerHandlerSuccessTests
{
    [Fact()]
    public async Task DeleteCustomerDetailsForExistingCustomerId_ReturnNoContent()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var mocker = new AutoMocker();

        var mockedRepository = mocker.GetMock<ICustomersRepository>();
        mockedRepository.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        var handler = mocker.CreateInstance<DeleteCustomerHandler>();
        var request = new DeleteCustomerRequest(id);

        // Act
        var result = await handler.ExecuteAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.NoContent);
    }

}
