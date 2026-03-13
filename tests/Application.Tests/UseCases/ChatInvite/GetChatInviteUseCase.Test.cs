using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetChatInviteUseCaseTest
{
    [Fact]
    public async Task Should_Return_ChatInvite_When_Found()
    {
        var chatInviteId = Guid.NewGuid();

        ChatInvite chatInvite = new ChatInviteMock(
            id: chatInviteId
        ).Create();

        var mockRepo = new Mock<IRepository<ChatInvite>>();
        mockRepo.Setup(r => r.GetById(chatInviteId))
                .ReturnsAsync(chatInvite);

        var service = new MockService<ChatInvite>(mockRepo.Object);
        var useCase = new GetChatInviteUseCase(service);

        var result = await useCase.Execute(chatInviteId);

        Assert.True(result.IsSuccess);
        Assert.Equal(chatInviteId, result.Value!.Id);
    }
}
