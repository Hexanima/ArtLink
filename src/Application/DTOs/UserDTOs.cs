namespace Application.DTOs;

public class CreateUserDTO
{
    public required string UserName { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UpdateUserDTO
{
    public required Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class UserDTO
{
    public required Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public class SecureUserDTO
{
    public required Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}