using Ardalis.Result;
using CustomersList.Application.DTO;
using CustomersList.Application.Repositories;
using CustomersList.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomersRepository _customerRepository;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ICustomersRepository customerRepository, ILogger<CustomerService> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<Result<Customer>> CreateCustomerAsync(CustomerDTO dto)
    {
        try
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            var createdCustomer = await _customerRepository.CreateAsync(customer);

            return Result<Customer>.Success(createdCustomer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the customer.");
            return Result<Customer>.Error("An error occurred while creating the customer.");
        }
    }

    public async Task<Result> DeleteCustomerAsync(Guid id)
    {
        try
        {
            var exists = await _customerRepository.ExistsAsync(id);
            if (!exists)
            {
                return Result.NotFound();
            }

            await _customerRepository.DeleteAsync(id);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the customer.");
            return Result.Error("An error occurred while deleting the customer.");
        }
    }

    public async Task<Result> ExistsAsync( Guid id )
    {
        try
        {
            var exists = await _customerRepository.ExistsAsync(id);
            return exists ? Result.Success() : Result.NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred trying to check a customer existence");
            return Result.Error("An error occurred trying to check a customer existence");
        }
    }

    public async Task<Result<Customer>> GetCustomerAsync(Guid id)
    {
        try
        {
            return Result<Customer>.Success(await _customerRepository.GetByIdAsync(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the customer.");
            return Result<Customer>.Error("An error occurred while getting the customer.");
        }
    }

    public async Task<Result<(IEnumerable<Customer>, int)>> GetCustomersAsync(int pageNumber, int pageSize)
    {
        try
        {
            return Result<(IEnumerable<Customer>, int)>.Success(await _customerRepository.GetListAsync(pageNumber, pageSize));
        }
        catch (Exception)
        {
            _logger.LogError("An error occurred while getting the list of customers.");
            return Result<(IEnumerable<Customer>, int)>.Error("An error occurred while getting the list of customers.");
        }
    }

    public async Task<Result> UpdateCustomerAsync(CustomerDTO customer, Guid id)
    {
        try
        {
            if (await _customerRepository.ExistsAsync(id) == false)
            {
                return Result.NotFound();
            }
            var entity = new Customer
            {
                Id = id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone
            };
            await _customerRepository.UpdateAsync(entity, id);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the customer.");
            return Result.Error("An error occurred while updating the customer.");
        }
    }
}