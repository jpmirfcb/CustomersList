using CustomersList.Api.Endpoints.Customers.Details;
using CustomersList.Api.Middleware;
using CustomersList.Application.DTO;
using CustomersList.Application.Models;
using CustomersList.Application.Services.Customers;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers.Create;

public class CreateCustomerEndpoint : Endpoint<CreateCustomerRequest, CreateCustomerResponse>
{
    private readonly ICustomerService _customersService;

    public CreateCustomerEndpoint( ICustomerService customersService )
    {
        _customersService = customersService;
    }

    public override void Configure()
    {
        Post("/customers");
        PreProcessor<ValidationsPreprocessor<CreateCustomerRequest>>();
    }

    public override async Task HandleAsync( CreateCustomerRequest req, CancellationToken ct )
    {

        var dto = new CustomerDTO
        {
            Name = req.Name,
            Email = req.Email,
            Phone = req.Phone
        };

        var createdCustomer = await _customersService.CreateCustomerAsync(dto);
        if(createdCustomer.IsSuccess )
        {
            var response = new CreateCustomerResponse
            {
                Id = createdCustomer.Value.Id,
                Name = createdCustomer.Value.Name,
                Email = createdCustomer.Value.Email,
                Phone = createdCustomer.Value.Phone
            };

            await SendCreatedAtAsync<CustomerDetailsEndpoint>( new {id = createdCustomer.Value.Id}, response);
            return;
        }
        else
        {
            createdCustomer.Errors.ToList().ForEach(error => AddError(error));
            await SendErrorsAsync();
            return;
        }
    }
}