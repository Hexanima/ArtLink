using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateChatUseCase
{
    private readonly IService<Chat> _service;

    public UpdateChatUseCase(IService<Chat> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Chat chat)
    {
        if (chat.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Chat Id cannot be empty."));
        }

        OperationResult<Chat> existingChatResult = await _service.GetById(chat.Id);

        if (!existingChatResult.IsSuccess)
        {
            return new OperationResult(new Exception("Chat not found"));
        }

        chat.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(chat);
    }
}
