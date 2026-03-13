using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetUsersUseCase
{
    private readonly IService<User> _service;

    public GetUsersUseCase(IService<User> service)
    {
        _service = service;
    }

    public async Task<OperationResult<User[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<User[]>(new Exception("No users found"));
        }

        return new OperationResult<User[]>(result.Value);
    }
}
