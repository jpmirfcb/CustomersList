using CustomersList.Api.Middleware;
using CustomersList.Application.DTO;
using CustomersList.Application.Models;
using CustomersList.Application.Services.Customers;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers.Update;

public class UpdateCustomerEndpoint : Endpoint<UpdateCustomerRequest>
{
    private readonly ICustomerService _customersService;

    public UpdateCustomerEndpoint( ICustomerService customersService )
    {
        _customersService = customersService;
    }

    public override void Configure()
    {
        Put("/customers/{id}");
        PreProcessor<ValidationsPreprocessor<UpdateCustomerRequest>>();
    }

    public override async Task HandleAsync( UpdateCustomerRequest req, CancellationToken ct )
    {
        var customer = new CustomerDTO()
        {
            Name = req.Name,
            Email = req.Email,
            Phone = req.Phone
        };

        if(!Guid.TryParse(req.Id, out var guid))
        {
            AddError("Invalid Id format");
            await SendErrorsAsync();
            return;
        }

        await _customersService.UpdateCustomerAsync(customer, guid);
        await SendNoContentAsync();
    }
}
