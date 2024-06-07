using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Customers.List;

public sealed class CustomersListHandler : UseCaseQuery<CustomersListRequest, CustomersListResponse, CustomersListRequestValidator>, ICustomersList
{
    private readonly ILogger<CustomersListHandler> _logger;
    private readonly ICustomersRepository _customersRepository;

    public CustomersListHandler( IMapper mapper, ILogger<CustomersListHandler> logger, ICustomersRepository customersRepository ) : base(mapper)
    {
        _logger = logger;
        _customersRepository = customersRepository;
    }

    protected override async Task<Result<CustomersListResponse>> HandleAsync( CustomersListRequest request, CancellationToken ct )
    {
        try
        {
            var result = await _customersRepository.GetListAsync(request.PageNumber, request.PageSize);

            return Result<CustomersListResponse>.Success(Mapper.Map<CustomersListResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the list of customers.");
            return Result<CustomersListResponse>.Error("An error occurred while getting the list of customers.");
        }
    }
}
