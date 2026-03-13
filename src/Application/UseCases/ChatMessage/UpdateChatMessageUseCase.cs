using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateChatMessageUseCase
{
    private readonly IService<ChatMessage> _service;

    public UpdateChatMessageUseCase(IService<ChatMessage> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(ChatMessage chatMessage)
    {
        if (chatMessage.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Chat message Id cannot be empty."));
        }

        OperationResult<ChatMessage> existingChatMessageResult = await _service.GetById(chatMessage.Id);

        if (!existingChatMessageResult.IsSuccess)
        {
            return new OperationResult(new Exception("Chat message not found"));
        }

        chatMessage.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(chatMessage);
    }
}
