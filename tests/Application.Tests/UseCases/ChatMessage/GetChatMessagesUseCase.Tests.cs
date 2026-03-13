using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetChatMessagesUseCaseTest
{
    [Fact]
    public async Task Should_Return_ChatMessages_When_Found()
    {
        ChatMessage[] chatMessages =
        {
            new ChatMessageMock().Create(),
            new ChatMessageMock().Create(),
            new ChatMessageMock().Create()
        };

        var mockRepo = new Mock<IRepository<ChatMessage>>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(chatMessages.ToList());

        var service = new MockService<ChatMessage>(mockRepo.Object);
        var useCase = new GetChatMessagesUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(chatMessages.Length, result.Value!.Length);
    }
}
