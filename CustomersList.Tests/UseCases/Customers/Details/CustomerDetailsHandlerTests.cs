using FluentAssertions;
using Moq.AutoMock;

namespace CustomersList.Application.UseCases.Customers.Details.Tests
{
    public class CustomerDetailsHandlerTests
    {
        [Fact()]
        public async Task GetCustomerDetailsForNonExistingCustomer_ReturnNotFoundResult()
        {

            // Arrange
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<CustomerDetailsHandler>();
            var request = new CustomerDetailsRequest( Guid.NewGuid().ToString() );

            // Act
            var result = await handler.ExecuteAsync(request, CancellationToken.None);
            
            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(Ardalis.Result.ResultStatus.NotFound);   

        }
    }
}