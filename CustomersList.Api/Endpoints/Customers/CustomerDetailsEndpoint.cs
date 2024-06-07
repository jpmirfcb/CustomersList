using Ardalis.Result;
using CustomersList.Api.Middleware;
using CustomersList.Application.Services.Customers;
using CustomersList.Application.UseCases.Customers.Details;
using CustomersList.Application.UseCases.Interfaces;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers;

public class CustomerDetailsEndpoint : Endpoint<CustomerDetailsRequest, CustomerDetailsResponse>
{
    private readonly ICustomerDetails _customerDetails;

    public CustomerDetailsEndpoint(ICustomerDetails customerDetails)
    {
        _customerDetails = customerDetails;
    }

    public override void Configure()
    {
        Get("/customers/{id}");
    }

    public override async Task HandleAsync(CustomerDetailsRequest req, CancellationToken ct)
    {

        var result = await _customerDetails.ExecuteAsync(req, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        else if (result.IsSuccess)
        {
            await SendAsync(new CustomerDetailsResponse
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                Email = result.Value.Email,
                Phone = result.Value.Phone
            }, cancellation: ct);
        }
        else
        {
            result.Errors.ToList().ForEach(error => AddError(error));
            await SendErrorsAsync(cancellation: ct);
        }
    }
}
