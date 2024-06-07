using Ardalis.Result;
using CustomersList.Application.UseCases.Customers.Details;
using CustomersList.Application.UseCases.Interfaces;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers;

public class CustomerDetailsEndpoint : Endpoint<CustomerDetailsRequest, CustomerDetailsResponse>
{
    private readonly ICustomerDetails _customerDetails;

    public CustomerDetailsEndpoint( ICustomerDetails customerDetails )
    {
        _customerDetails = customerDetails;
    }

    public override void Configure()
    {
        Get("/customers/{id}");
    }

    public override async Task HandleAsync( CustomerDetailsRequest req, CancellationToken ct )
    {

        var result = await _customerDetails.ExecuteAsync(req, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        else if (result.IsSuccess)
        {
            await SendAsync(result, cancellation: ct);
        }
        else
        {
            result.Errors.ToList().ForEach(error => AddError(error));
            await SendErrorsAsync(cancellation: ct);
        }
    }
}
