using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetChatUserUseCase
{
    private readonly IService<ChatUser> _service;

    public GetChatUserUseCase(IService<ChatUser> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ChatUser>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<ChatUser>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ChatUser>(new Exception("Chat user not found"));
        }

        return new OperationResult<ChatUser>(result.Value);
    }
}
