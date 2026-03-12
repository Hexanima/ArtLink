using Domain.Entities;

namespace Application.Mocks.Entities;

public static class MockUser
{
    public static User Generate()
    {
        return new()
        {
            FullName = "John Doe",
            Email = "test@test.com",
            HashedPassword = "a",
            UserName = "JohnDoe123",
            Id = new(),
            CreatedAt = new(),
            UpdatedAt = new()
        };
    }
}
