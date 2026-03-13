using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetUserUseCase
{
    private readonly IService<User> _service;

    public GetUserUseCase(IService<User> service)
    {
        _service = service;
    }

    public async Task<OperationResult<User>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<User>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<User>(new Exception("User not found"));
        }

        return new OperationResult<User>(result.Value);
    }
}
