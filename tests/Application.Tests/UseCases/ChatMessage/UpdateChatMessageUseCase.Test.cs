using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateChatMessageUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_ChatMessage_Is_Updated()
    {
        var chatMessageId = Guid.NewGuid();

        ChatMessage chatMessage = new ChatMessageMock(
            id: chatMessageId,
            createdAt: DateTime.UtcNow.AddMinutes(-1),
            updatedAt: DateTime.UtcNow.AddMinutes(-1)
        ).Create();

        var mockRepo = new Mock<IRepository<ChatMessage>>();
        mockRepo.Setup(r => r.GetById(chatMessageId)).ReturnsAsync(chatMessage);
        mockRepo.Setup(r => r.Update(It.IsAny<ChatMessage>(), chatMessageId))
                .Callback<ChatMessage, Guid>((updated, id) => chatMessage = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ChatMessage>(mockRepo.Object);
        var useCase = new UpdateChatMessageUseCase(service);

        chatMessage.Message = "Updated message";
        await useCase.Execute(chatMessage);

        Assert.Equal("Updated message", chatMessage.Message);
        Assert.True(chatMessage.UpdatedAt > chatMessage.CreatedAt);
    }
}
