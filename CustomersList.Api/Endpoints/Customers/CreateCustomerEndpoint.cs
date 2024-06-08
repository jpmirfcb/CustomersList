using Ardalis.Result;
using CustomersList.Api.Extensions;
using CustomersList.Application.UseCases.Customers.Create;
using CustomersList.Application.UseCases.Interfaces;
using FastEndpoints;
using System.Threading;

namespace CustomersList.Api.Endpoints.Customers;

public class CreateCustomerEndpoint : Endpoint<CreateCustomerRequest, CreateCustomerResponse>
{
    private readonly ICreateCustomer _createCustomer;

    public CreateCustomerEndpoint( ICreateCustomer createCustomer )
    {
        _createCustomer = createCustomer;
    }

    public override void Configure()
    {
        Post("/customers");
    }

    public override async Task HandleAsync(CreateCustomerRequest req, CancellationToken ct)
    {
        var result = await _createCustomer.ExecuteAsync(req, ct);
        if (result.IsSuccess)
        {
            await SendCreatedAtAsync<CustomerDetailsEndpoint>(new { id = result.Value.Id }, result.Value, cancellation: ct);
        }
        else
        {
            await this.SendResultErrors(result, ct);
        }
    }
}