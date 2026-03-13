using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateChatInviteUseCase
{
    private readonly IService<ChatInvite> _service;

    public UpdateChatInviteUseCase(IService<ChatInvite> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(ChatInvite chatInvite)
    {
        if (chatInvite.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Chat invite Id cannot be empty."));
        }

        OperationResult<ChatInvite> existingChatInviteResult = await _service.GetById(chatInvite.Id);

        if (!existingChatInviteResult.IsSuccess)
        {
            return new OperationResult(new Exception("Chat invite not found"));
        }

        chatInvite.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(chatInvite);
    }
}
