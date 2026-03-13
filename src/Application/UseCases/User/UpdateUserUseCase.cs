using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateUserUseCase
{
    private readonly IService<User> _service;

    public UpdateUserUseCase(IService<User> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(User user)
    {
        if (user.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("User Id cannot be empty."));
        }

        OperationResult<User> existingUserResult = await _service.GetById(user.Id);

        if (!existingUserResult.IsSuccess)
        {
            return new OperationResult(new Exception("User not found"));
        }

        user.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(user);
    }
}
