using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateChatUserUseCase
{
    private readonly IService<ChatUser> _service;

    public UpdateChatUserUseCase(IService<ChatUser> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(ChatUser chatUser)
    {
        if (chatUser.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Chat user Id cannot be empty."));
        }

        OperationResult<ChatUser> existingChatUserResult = await _service.GetById(chatUser.Id);

        if (!existingChatUserResult.IsSuccess)
        {
            return new OperationResult(new Exception("Chat user not found"));
        }

        chatUser.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(chatUser);
    }
}
