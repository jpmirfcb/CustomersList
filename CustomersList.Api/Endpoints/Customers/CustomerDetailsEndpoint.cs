using Ardalis.Result;
using CustomersList.Api.Extensions;
using CustomersList.Application.UseCases.Customers.Details;
using CustomersList.Application.UseCases.Interfaces;
using FastEndpoints;
using System.Threading;

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
            await this.SendResultErrors(result, ct);
        }
    }
}
