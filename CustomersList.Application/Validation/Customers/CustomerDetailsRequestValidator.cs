using CustomersList.Application.Models;
using CustomersList.Application.Services.Customers;
using FluentValidation;

namespace CustomersList.Application.Validation.Customers;

public class CustomerDetailsRequestValidator : AbstractValidator<CustomerDetailsRequest>
{
    public CustomerDetailsRequestValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Id is invalid");
    }
}
