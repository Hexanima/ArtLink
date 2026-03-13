using Domain.Entities;

namespace Tests.Mocks.Entities;
public class UserMock
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }

    public UserMock(
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null,
        string? userName = null,
        string? fullName = null,
        string? email = null,
        string? hashedPassword = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        DeletedAt = deletedAt;
        UserName = userName ?? "jdoe";
        FullName = fullName ?? "John Doe";
        Email = email ?? "john@test.com";
        HashedPassword = hashedPassword ?? "hashed-password";
    }

    public User Create()
    {
        User result = new User
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt,
            UserName = UserName,
            FullName = FullName,
            Email = Email,
            HashedPassword = HashedPassword
        };

        return result;
    }
}
