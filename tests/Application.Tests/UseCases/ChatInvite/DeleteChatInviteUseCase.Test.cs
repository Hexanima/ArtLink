using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteChatInviteUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_ChatInvite_Is_Deleted()
    {
        var chatInviteId = Guid.NewGuid();

        ChatInvite chatInvite = new ChatInviteMock(
            id: chatInviteId
        ).Create();

        var mockRepo = new Mock<IRepository<ChatInvite>>();
        mockRepo.Setup(r => r.GetById(chatInviteId)).ReturnsAsync(chatInvite);
        mockRepo.Setup(r => r.Update(It.IsAny<ChatInvite>(), chatInviteId))
                .Callback<ChatInvite, Guid>((updated, id) => chatInvite = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ChatInvite>(mockRepo.Object);
        var useCase = new DeleteChatInviteUseCase(service);

        await useCase.Execute(chatInviteId);

        Assert.NotNull(chatInvite.DeletedAt);
        Assert.True(chatInvite.DeletedAt <= DateTime.UtcNow);
    }
}
