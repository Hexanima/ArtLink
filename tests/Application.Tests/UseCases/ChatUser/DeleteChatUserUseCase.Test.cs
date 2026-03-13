using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteChatUserUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_ChatUser_Is_Deleted()
    {
        var chatUserId = Guid.NewGuid();

        ChatUser chatUser = new ChatUserMock(
            id: chatUserId
        ).Create();

        var mockRepo = new Mock<IRepository<ChatUser>>();
        mockRepo.Setup(r => r.GetById(chatUserId)).ReturnsAsync(chatUser);
        mockRepo.Setup(r => r.Update(It.IsAny<ChatUser>(), chatUserId))
                .Callback<ChatUser, Guid>((updated, id) => chatUser = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ChatUser>(mockRepo.Object);
        var useCase = new DeleteChatUserUseCase(service);

        await useCase.Execute(chatUserId);

        Assert.NotNull(chatUser.DeletedAt);
        Assert.True(chatUser.DeletedAt <= DateTime.UtcNow);
    }
}
