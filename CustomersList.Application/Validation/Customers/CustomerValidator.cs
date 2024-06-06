using CustomersList.Domain.Entities;
using FluentValidation;

namespace CustomersList.Application.Validation.Customers;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {

        RuleFor(x=> x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(x=> x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x=> x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");

        RuleFor(x=> x.Phone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^\d{10}$").WithMessage("Phone is invalid");
    }
}
