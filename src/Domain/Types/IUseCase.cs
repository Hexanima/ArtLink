using Domain.Types;

public interface IUseCase<TPayload, TResult>
{
    public Task<OperationResult<TResult>> Execute(TPayload payload);
}
