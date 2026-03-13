using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetChatInvitesUseCase
{
    private readonly IService<ChatInvite> _service;

    public GetChatInvitesUseCase(IService<ChatInvite> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ChatInvite[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ChatInvite[]>(new Exception("No chat invites found"));
        }

        return new OperationResult<ChatInvite[]>(result.Value);
    }
}
