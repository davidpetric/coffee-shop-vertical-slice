namespace Application.Domain.Users;

using CSharpFunctionalExtensions;

public class Employee : Entity
{
    public long? UserId { get; set; }

    public User? User { get; set; }
}
