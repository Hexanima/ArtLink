using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;

namespace Application.UseCases.Base.Implementation;

public class CreateUseCase<T> : ICreateUseCase<T> where T : class, IEntity
{
    private readonly IService<T> _service;

    public CreateUseCase(IService<T> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(T entity)
    {
        // La validación de campos requeridos la realiza el servicio
        // El mapeo DTO → Entidad en el controller se encarga de inyectar valores válidos
        var result = await _service.Create(entity);
        
        if (!result.IsSuccess)
        {
            return new OperationResult(result.Error);
        }
        
        return new OperationResult();
    }
}

