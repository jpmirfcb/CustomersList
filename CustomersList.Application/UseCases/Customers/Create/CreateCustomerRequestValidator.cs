using FluentValidation;

namespace CustomersList.Application.UseCases.Customers.Create;

public sealed class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid")
            .MaximumLength(255).WithMessage("Name must not exceed 255 characters");

        RuleFor(x => x.Phone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^\+\d{12,14}$").WithMessage("Phone is invalid. It must start with a + sign followed by 12 to 14 digits");
    }
}
