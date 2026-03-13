using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteUserUseCase
{
    private readonly IService<User> _service;

    public DeleteUserUseCase(IService<User> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<User> existingUserResult = await _service.GetById(id);

        if (!existingUserResult.IsSuccess)
        {
            return new OperationResult(new Exception("User not found"));
        }

        return await _service.Delete(id);
    }
}
