using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateChatInviteUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_ChatInvite_Is_Updated()
    {
        var chatInviteId = Guid.NewGuid();

        ChatInvite chatInvite = new ChatInviteMock(
            id: chatInviteId,
            createdAt: DateTime.UtcNow.AddMinutes(-1),
            updatedAt: DateTime.UtcNow.AddMinutes(-1)
        ).Create();

        var mockRepo = new Mock<IRepository<ChatInvite>>();
        mockRepo.Setup(r => r.GetById(chatInviteId)).ReturnsAsync(chatInvite);
        mockRepo.Setup(r => r.Update(It.IsAny<ChatInvite>(), chatInviteId))
                .Callback<ChatInvite, Guid>((updated, id) => chatInvite = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ChatInvite>(mockRepo.Object);
        var useCase = new UpdateChatInviteUseCase(service);

        await useCase.Execute(chatInvite);

        Assert.True(chatInvite.UpdatedAt > chatInvite.CreatedAt);
    }
}
