using Ardalis.Result;

namespace CustomersList.Application.UseCases.Abstractions;

public interface IUseCaseCommand<TRequest, TValidator>
{
    Task<Result> ExecuteAsync( TRequest request, CancellationToken ct );
}
