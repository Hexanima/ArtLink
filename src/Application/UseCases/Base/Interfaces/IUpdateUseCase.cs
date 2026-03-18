using Domain.Types;

namespace Application.UseCases.Base.Interfaces;

public interface IUpdateUseCase<T> where T : IEntity
{
    Task<OperationResult> Execute(T entity);
}
