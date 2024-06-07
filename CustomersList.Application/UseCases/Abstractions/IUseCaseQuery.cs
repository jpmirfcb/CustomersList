using Ardalis.Result;

namespace CustomersList.Application.UseCases.Abstractions;

public interface IUseCaseQuery<TRequest, TResponse, TValidator>
{
    Task<Result<TResponse>> ExecuteAsync( TRequest request, CancellationToken ct );
}
