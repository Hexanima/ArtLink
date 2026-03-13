using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetChatUsersUseCase
{
    private readonly IService<ChatUser> _service;

    public GetChatUsersUseCase(IService<ChatUser> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ChatUser[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ChatUser[]>(new Exception("No chat users found"));
        }

        return new OperationResult<ChatUser[]>(result.Value);
    }
}
