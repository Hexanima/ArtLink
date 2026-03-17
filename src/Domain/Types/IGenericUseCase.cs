using Domain.Types;

public interface IGetByIdUseCase<T> where T : IEntity
{
    Task<OperationResult<T>> Execute(Guid id);
}

public interface IDeleteUseCase<T> where T : IEntity
{
    Task<OperationResult<T>> Execute(Guid id);
}