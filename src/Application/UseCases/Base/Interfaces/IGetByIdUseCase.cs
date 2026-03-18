using Domain.Types;

namespace Application.UseCases.Base.Interfaces;

public interface IGetByIdUseCase<T> where T : IEntity
{
    Task<OperationResult<T>> Execute(Guid id);
}
