using Domain.Types;

namespace Application.UseCases.Base.Interfaces;

public interface IGetAllUseCase<T> where T : IEntity
{
    Task<OperationResult<T[]>> Execute();
}
