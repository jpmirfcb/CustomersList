using Ardalis.Result;
using CustomersList.Api.Middleware;
using CustomersList.Application.Models;
using CustomersList.Application.Services.Customers;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers.Details;

public class CustomerDetailsEndpoint : Endpoint<CustomerDetailsRequest, CustomerDetailsResponse>
{
    private readonly ICustomerService _customersService;

    public CustomerDetailsEndpoint( ICustomerService customersService )
    {
        _customersService = customersService;
    }

    public override void Configure()
    {
        Get("/customers/{id}");
        PreProcessor<ValidationsPreprocessor<CustomerDetailsRequest>>();
    }

    public override async Task HandleAsync( CustomerDetailsRequest req, CancellationToken ct )
    {

        if (Guid.TryParse(req.Id, out Guid id) == false)
        {
            AddError("Invalid customer id.");
            await SendErrorsAsync();
            return;
        }

        var result = await _customersService.GetCustomerAsync(id);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        else
        {
            await SendAsync(new CustomerDetailsResponse
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                Email = result.Value.Email,
                Phone = result.Value.Phone
            });
        }
    }
}
