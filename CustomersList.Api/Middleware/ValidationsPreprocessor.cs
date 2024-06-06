using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;

namespace CustomersList.Api.Middleware;

public class ValidationsPreprocessor<TRequest> : IPreProcessor<TRequest>
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationsPreprocessor( IServiceProvider serviceProvider )
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PreProcessAsync( IPreProcessorContext<TRequest> ctx, CancellationToken ct )
    {
        using var scope = _serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;

        var validatorType = typeof(IValidator<>).MakeGenericType(ctx.Request.GetType());
        var validator = provider.GetService(validatorType) as IValidator<TRequest>;

        if (validator != null)
        {
            var validationContext = new FluentValidation.ValidationContext<TRequest>(ctx.Request);
            var validationResult = await validator.ValidateAsync(validationContext, ct);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ctx.ValidationFailures.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage));
                }

                await ctx.HttpContext.Response.SendErrorsAsync(ctx.ValidationFailures);
                return;
            }
        }
    }
}
