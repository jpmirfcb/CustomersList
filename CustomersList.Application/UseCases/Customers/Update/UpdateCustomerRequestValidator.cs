﻿using FluentValidation;

namespace CustomersList.Application.UseCases.Customers.Update;

public sealed class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Id is required")
            .Must(x => !x.Equals(Guid.Empty.ToString())).WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out var _)).WithMessage("Invalid Id");

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters")
            .EmailAddress().WithMessage("Email is invalid");
    }
}
