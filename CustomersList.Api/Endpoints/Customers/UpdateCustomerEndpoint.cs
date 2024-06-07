using CustomersList.Application.UseCases.Customers.Update;
using CustomersList.Application.UseCases.Interfaces;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers;

public class UpdateCustomerEndpoint : Endpoint<UpdateCustomerRequest>
{
    private readonly IUpdateCustomer _updateCustomer;

    public UpdateCustomerEndpoint(IUpdateCustomer updateCustomer)
    {
        _updateCustomer = updateCustomer;
    }

    public override void Configure()
    {
        Put("/customers/{id}");
    }

    public override async Task HandleAsync(UpdateCustomerRequest req, CancellationToken ct)
    {
        var result = await _updateCustomer.ExecuteAsync(req, ct);
        if (result.IsSuccess)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            result.Errors.ToList().ForEach(error => AddError(error));
            await SendErrorsAsync(cancellation: ct);
        }
    }
}
