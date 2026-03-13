using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteChatInviteUseCase
{
    private readonly IService<ChatInvite> _service;

    public DeleteChatInviteUseCase(IService<ChatInvite> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<ChatInvite> existingChatInviteResult = await _service.GetById(id);

        if (!existingChatInviteResult.IsSuccess)
        {
            return new OperationResult(new Exception("Chat invite not found"));
        }

        return await _service.Delete(id);
    }
}
