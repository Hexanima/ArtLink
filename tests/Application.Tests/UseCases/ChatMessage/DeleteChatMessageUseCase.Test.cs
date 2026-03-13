using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteChatMessageUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_ChatMessage_Is_Deleted()
    {
        var chatMessageId = Guid.NewGuid();

        ChatMessage chatMessage = new ChatMessageMock(
            id: chatMessageId
        ).Create();

        var mockRepo = new Mock<IRepository<ChatMessage>>();
        mockRepo.Setup(r => r.GetById(chatMessageId)).ReturnsAsync(chatMessage);
        mockRepo.Setup(r => r.Update(It.IsAny<ChatMessage>(), chatMessageId))
                .Callback<ChatMessage, Guid>((updated, id) => chatMessage = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ChatMessage>(mockRepo.Object);
        var useCase = new DeleteChatMessageUseCase(service);

        await useCase.Execute(chatMessageId);

        Assert.NotNull(chatMessage.DeletedAt);
        Assert.True(chatMessage.DeletedAt <= DateTime.UtcNow);
    }
}
