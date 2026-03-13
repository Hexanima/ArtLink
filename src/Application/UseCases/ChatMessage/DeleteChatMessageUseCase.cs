using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteChatMessageUseCase
{
    private readonly IService<ChatMessage> _service;

    public DeleteChatMessageUseCase(IService<ChatMessage> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<ChatMessage> existingChatMessageResult = await _service.GetById(id);

        if (!existingChatMessageResult.IsSuccess)
        {
            return new OperationResult(new Exception("Chat message not found"));
        }

        return await _service.Delete(id);
    }
}
