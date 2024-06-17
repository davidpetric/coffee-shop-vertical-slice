namespace Application.Domain.Users;

using CSharpFunctionalExtensions;

public class Customer : Entity
{
    public long? UserId { get; set; }

    public User? User { get; set; }
}
