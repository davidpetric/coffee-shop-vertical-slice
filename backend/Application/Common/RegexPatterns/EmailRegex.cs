namespace Application.Common.Regexes;

using System.Text.RegularExpressions;

public static partial class EmailRegex
{
    private static readonly Regex pattern = EmailRegexPattern();

    public static bool IsValidEmail(string email)
    {
        return pattern.IsMatch(email);
    }

    [GeneratedRegex(
        pattern: "^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$",
        RegexOptions.None,
        matchTimeoutMilliseconds: 1000
    )]
    private static partial Regex EmailRegexPattern();
}
