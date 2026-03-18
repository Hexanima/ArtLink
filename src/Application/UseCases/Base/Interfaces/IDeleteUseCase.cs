using Domain.Types;

namespace Application.UseCases.Base.Interfaces;

public interface IDeleteUseCase<T> where T : IEntity
{
    Task<OperationResult> Execute(Guid id);
}
