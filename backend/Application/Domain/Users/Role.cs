namespace Application.Domain.Users;

using CSharpFunctionalExtensions;

public class Role : Entity
{
    public Role()
    {

    }

    public Role(long id) : base(id)
    {

    }

    public required string Name { get; set; }

    public List<User> Users { get; set; } = [];

    internal void SetId(long id)
    {
        this.Id = id;
    }
}