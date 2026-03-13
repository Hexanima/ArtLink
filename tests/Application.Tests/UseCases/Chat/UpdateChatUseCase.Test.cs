using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateChatUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_Chat_Is_Updated()
    {
        var chatId = Guid.NewGuid();

        Chat chat = new ChatMock(
            id: chatId,
            createdAt: DateTime.UtcNow.AddMinutes(-1),
            updatedAt: DateTime.UtcNow.AddMinutes(-1)
        ).Create();

        var mockRepo = new Mock<IRepository<Chat>>();
        mockRepo.Setup(r => r.GetById(chatId)).ReturnsAsync(chat);
        mockRepo.Setup(r => r.Update(It.IsAny<Chat>(), chatId))
                .Callback<Chat, Guid>((updated, id) => chat = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<Chat>(mockRepo.Object);
        var useCase = new UpdateChatUseCase(service);

        chat.Title = "Updated chat title";
        await useCase.Execute(chat);

        Assert.Equal("Updated chat title", chat.Title);
        Assert.True(chat.UpdatedAt > chat.CreatedAt);
    }
}
