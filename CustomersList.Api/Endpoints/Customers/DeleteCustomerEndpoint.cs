using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Customers.Delete;
using CustomersList.Application.UseCases.Interfaces;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers;

public class DeleteCustomerEndpoint : Endpoint<DeleteCustomerRequest>
{

    private readonly IDeleteCustomer _deleteCustomer;

    public DeleteCustomerEndpoint(IDeleteCustomer deleteCustomer)
    {
        _deleteCustomer = deleteCustomer;
    }

    public override void Configure()
    {
        Delete("/customers/{id}");
    }

    public override async Task HandleAsync(DeleteCustomerRequest req, CancellationToken ct)
    {
        var result = await _deleteCustomer.ExecuteAsync(req, ct);

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
