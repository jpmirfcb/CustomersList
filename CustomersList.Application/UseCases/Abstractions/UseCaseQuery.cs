using Ardalis.Result;
using AutoMapper;
using FluentValidation;

namespace CustomersList.Application.UseCases.Abstractions;

public abstract class UseCaseQuery<TRequest, TResponse, TValidator>: UseCaseBase
    where TRequest : class
    where TResponse : class
    where TValidator : class, IValidator<TRequest>
{
    protected UseCaseQuery( IMapper mapper ) : base(mapper)
    {
    }

    public async Task<Result<TResponse>> ExecuteAsync( TRequest request, CancellationToken ct )
    {
        var validationResult = await ValidateAsync(request, ct);
        if (!validationResult.IsSuccess)
        {
            return Result<TResponse>.Invalid(validationResult.ValidationErrors);
        }

        return await HandleAsync(request, ct);
    }

    protected abstract Task<Result<TResponse>> HandleAsync( TRequest request, CancellationToken ct );


    protected async Task<Result> ValidateAsync( TRequest request, CancellationToken ct )
    {
        var validator = Activator.CreateInstance<TValidator>();
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            var failures = new List<ValidationError>();
            foreach (var error in validationResult.Errors)
            {
                failures.Add(new ValidationError(error.PropertyName, error.ErrorMessage, errorCode: string.Empty, ValidationSeverity.Error));
            }
            return Result.Invalid(failures);
        }
        return Result.Success();
    }
}
