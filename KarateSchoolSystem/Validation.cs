using System.Text.RegularExpressions;

namespace KarateSchoolSystem;

/// <summary>Shared validation helpers for domain classes.</summary>
internal static class Validation
{
    public static void RequirePositive(int value, string name)
    {
        if (value <= 0) throw new ArgumentException($"{name} must be greater than zero.", name);
    }

    public static string RequireText(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"{name} is required.", name);
        return value.Trim();
    }

    public static string RequireEmail(string? email)
    {
        string value = RequireText(email, nameof(email));
        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Email format is invalid.", nameof(email));
        return value;
    }

    public static void RequireNotFuture(DateTime date, string name)
    {
        if (date.Date > DateTime.Today) throw new ArgumentException($"{name} cannot be in the future.", name);
    }
}
