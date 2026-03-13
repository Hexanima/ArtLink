using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetChatInviteUseCase
{
    private readonly IService<ChatInvite> _service;

    public GetChatInviteUseCase(IService<ChatInvite> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ChatInvite>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<ChatInvite>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ChatInvite>(new Exception("Chat invite not found"));
        }

        return new OperationResult<ChatInvite>(result.Value);
    }
}
