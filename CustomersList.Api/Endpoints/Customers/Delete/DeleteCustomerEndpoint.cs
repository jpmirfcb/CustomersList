using Ardalis.Result;
using CustomersList.Api.Middleware;
using CustomersList.Application.Models;
using CustomersList.Application.Services.Customers;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers.Delete;


public class DeleteCustomerEndpoint : Endpoint<DeleteCustomerRequest>
{
    private readonly ICustomerService _customersService;

    public DeleteCustomerEndpoint( ICustomerService customersService )
    {
        _customersService = customersService;
    }

    public override void Configure()
    {
        Delete("/customers/{id}");
        PreProcessor<ValidationsPreprocessor<DeleteCustomerRequest>>();
    }

    public override async Task HandleAsync(DeleteCustomerRequest req, CancellationToken ct)
    {
        var id = Guid.Parse(req.Id);

        var result = await _customersService.DeleteCustomerAsync(id);
        
        if(result.IsNotFound())
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (result.IsSuccess)
        {
            await SendNoContentAsync();
        }
        else
        {
            result.Errors.ToList().ForEach(error => AddError(error));
            await SendErrorsAsync();
        }
    
    }
}
