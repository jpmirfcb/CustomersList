using Ardalis.Result;
using FastEndpoints;

namespace CustomersList.Api.Extensions;

public static class EndpointExtensions
{
    public static async Task SendResultErrors<TRequest, TResponse, TResult>( this Endpoint<TRequest, TResponse> endpoint, Result<TResult> result, CancellationToken cancellationToken )
    {
        result.Errors.ToList().ForEach(error => endpoint.AddError(error));
        result.ValidationErrors.ToList().ForEach(error => endpoint.ValidationFailures.Add(new(error.Identifier, error.ErrorMessage)));
        await endpoint.HttpContext.Response.SendErrorsAsync(endpoint.ValidationFailures);
    }

    public static async Task SendResultErrors<TRequest, TResult>( this Endpoint<TRequest> endpoint, Result<TResult> result, CancellationToken cancellationToken )
    {
        result.Errors.ToList().ForEach(error => endpoint.AddError(error));
        result.ValidationErrors.ToList().ForEach(error => endpoint.ValidationFailures.Add(new(error.Identifier, error.ErrorMessage)));
        await endpoint.HttpContext.Response.SendErrorsAsync(endpoint.ValidationFailures);
    }

}
