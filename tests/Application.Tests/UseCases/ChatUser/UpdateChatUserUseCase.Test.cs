using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateChatUserUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_ChatUser_Is_Updated()
    {
        var chatUserId = Guid.NewGuid();

        ChatUser chatUser = new ChatUserMock(
            id: chatUserId,
            createdAt: DateTime.UtcNow.AddMinutes(-1),
            updatedAt: DateTime.UtcNow.AddMinutes(-1),
            isAdmin: false
        ).Create();

        var mockRepo = new Mock<IRepository<ChatUser>>();
        mockRepo.Setup(r => r.GetById(chatUserId)).ReturnsAsync(chatUser);
        mockRepo.Setup(r => r.Update(It.IsAny<ChatUser>(), chatUserId))
                .Callback<ChatUser, Guid>((updated, id) => chatUser = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ChatUser>(mockRepo.Object);
        var useCase = new UpdateChatUserUseCase(service);

        chatUser.IsAdmin = true;
        await useCase.Execute(chatUser);

        Assert.True(chatUser.IsAdmin);
        Assert.True(chatUser.UpdatedAt > chatUser.CreatedAt);
    }
}
