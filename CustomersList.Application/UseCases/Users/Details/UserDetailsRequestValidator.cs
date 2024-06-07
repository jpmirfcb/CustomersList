using FluentValidation;

namespace CustomersList.Application.UseCases.Users.Details;

public sealed class UserDetailsRequestValidator : AbstractValidator<UserDetailsRequest>
{
    public UserDetailsRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotNull().WithMessage("Id is required")
            .NotEqual(x => Guid.Empty).WithMessage("Invalid Id");
    }
}
