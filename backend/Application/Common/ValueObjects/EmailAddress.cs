namespace Application.Common.ValueObjects;

using Application.Common.Regexes;

public record EmailAddress
{
    public string Email { get; private set; } = default!;

    public bool Create(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        if (!EmailRegex.IsValidEmail(email))
        {
            return false;
        }

        Email = email;
        return true;
    }
}
