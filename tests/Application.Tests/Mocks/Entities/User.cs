namespace Application.Mocks.Entities;

public static class MockUser
{
    public static User Generate()
    {
        return new()
        {
            Email = "test@test.com",
            HashedPassword = "a",
            UserName = "JohnDoe123",
            Id = new(),
            CreatedAt = new(),
            UpdatedAt = new()
        };
    }
}
