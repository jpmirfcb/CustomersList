using FluentValidation;

namespace CustomersList.Application.UseCases.Customers.List;

public sealed class CustomersListRequestValidator : AbstractValidator<CustomersListRequest>
{
    public CustomersListRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(1).WithMessage("Page size must be greater than or equal to 1")
            .Must(x => x == 10 || x == 20 || x == 50).WithMessage("Page size can only be set to 10, 20 or 50");
    }
}
