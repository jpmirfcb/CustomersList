using FluentValidation;

namespace CustomersList.Application.UseCases.Customers.Delete;

public sealed class DeleteCustomerRequestValidator : AbstractValidator<DeleteCustomerRequest>
{
    public DeleteCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Id is required")
            .Must(x => !x.Equals(Guid.Empty.ToString())).WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Id is invalid");
    }
}
