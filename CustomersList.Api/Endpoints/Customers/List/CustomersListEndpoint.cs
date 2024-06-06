using CustomersList.Api.Middleware;
using CustomersList.Application.Models;
using CustomersList.Application.Services.Customers;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers.List;

public class CustomersListEndpoint : Endpoint<CustomerListRequest, CustomerListResponse>
{
    private readonly ICustomerService _customersService;
    public CustomersListEndpoint( ICustomerService customersService )
    {
        _customersService = customersService;
    }

    public override void Configure()
    {
        Get("/customers");
        Summary(x =>
        {
            x.Summary = "Get a list of customers";
            x.RequestParam(r => r.PageNumber, "The page number to retrieve");
            x.RequestParam(r => r.PageSize, "The number of items per page");
        });
        PreProcessor<ValidationsPreprocessor<CustomerListRequest>>();
    }

    public override async Task HandleAsync( CustomerListRequest request, CancellationToken ct )
    {
        var response = await _customersService.GetCustomersAsync(request.PageNumber, request.PageSize);

        if (!response.IsSuccess) await SendErrorsAsync();

        var customerListResponse = new CustomerListResponse()
        {
            Customers = response.Value.Item1,
            TotalRecords = response.Value.Item2
        };
        await SendOkAsync(customerListResponse);
    }
}