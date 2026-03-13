using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetChatMessageUseCaseTest
{
    [Fact]
    public async Task Should_Return_ChatMessage_When_Found()
    {
        var chatMessageId = Guid.NewGuid();

        ChatMessage chatMessage = new ChatMessageMock(
            id: chatMessageId
        ).Create();

        var mockRepo = new Mock<IRepository<ChatMessage>>();
        mockRepo.Setup(r => r.GetById(chatMessageId))
                .ReturnsAsync(chatMessage);

        var service = new MockService<ChatMessage>(mockRepo.Object);
        var useCase = new GetChatMessageUseCase(service);

        var result = await useCase.Execute(chatMessageId);

        Assert.True(result.IsSuccess);
        Assert.Equal(chatMessageId, result.Value!.Id);
    }
}
