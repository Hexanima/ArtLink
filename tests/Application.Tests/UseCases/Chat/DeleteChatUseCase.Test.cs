using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteChatUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_Chat_Is_Deleted()
    {
        var chatId = Guid.NewGuid();

        Chat chat = new ChatMock(
            id: chatId
        ).Create();

        var mockRepo = new Mock<IRepository<Chat>>();
        mockRepo.Setup(r => r.GetById(chatId)).ReturnsAsync(chat);
        mockRepo.Setup(r => r.Update(It.IsAny<Chat>(), chatId))
                .Callback<Chat, Guid>((updated, id) => chat = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<Chat>(mockRepo.Object);
        var useCase = new DeleteChatUseCase(service);

        await useCase.Execute(chatId);

        Assert.NotNull(chat.DeletedAt);
        Assert.True(chat.DeletedAt <= DateTime.UtcNow);
    }
}
