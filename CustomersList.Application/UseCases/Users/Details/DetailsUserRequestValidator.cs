using FluentValidation;

namespace CustomersList.Application.UseCases.Users.Details;

public sealed class DetailsUserRequestValidator : AbstractValidator<DetailsUserRequest>
{
    public DetailsUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotNull().WithMessage("Id is required")
            .NotEqual(x => Guid.Empty).WithMessage("Invalid Id");
    }
}
