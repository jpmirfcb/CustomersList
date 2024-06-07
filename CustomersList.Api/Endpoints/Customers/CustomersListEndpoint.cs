using CustomersList.Application.UseCases.Customers.List;
using CustomersList.Application.UseCases.Interfaces;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Customers;

public class CustomersListEndpoint : Endpoint<CustomersListRequest, CustomersListResponse>
{
    private readonly ICustomersList _customersList;

    public CustomersListEndpoint( ICustomersList customersList )
    {
        _customersList = customersList;
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
    }

    public override async Task HandleAsync( CustomersListRequest request, CancellationToken ct )
    {
        var result = await _customersList.ExecuteAsync(request, ct);
        if (result.IsSuccess)
        {
            await SendOkAsync(result.Value, cancellation: ct);
        }
        else
        {
            result.Errors.ToList().ForEach(error => AddError(error));
            await SendErrorsAsync(cancellation: ct);
        }
    }
}