namespace Application.Domain.Users;

using Application.Common.ValueObjects;

using CSharpFunctionalExtensions;

public class User : Entity
{
    public User()
    {
    }

    public User(long id) : base(id)
    {
    }

    public EmailAddress EmailAddress { get; set; } = default!;

    public required string FirstName { get; set; } = default!;

    public string? MiddleName { get; set; }

    public required string LastName { get; set; } = default!;

    public bool CanLogin { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset? LastLogin { get; set; }

    public List<Role> Roles { get; } = [];

    public long? EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    public Customer? Customer { get; set; }

    public string GetDisplayName() => $"{FirstName} {LastName}".Trim();
}
