namespace KarateSchoolSystem;

/// <summary>Abstract base class for every person with a system account.</summary>
public abstract class User
{
    public int UserId { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public int Age { get; private set; }
    public UserRole Role { get; }
    public DateTime CreatedAt { get; }

    protected User(int userId, string firstName, string lastName, string email, int age, UserRole role)
    {
        Validation.RequirePositive(userId, nameof(userId));
        if (age < 16) throw new ArgumentException("Age must be at least 16.", nameof(age));
        UserId = userId;
        FirstName = Validation.RequireText(firstName, nameof(firstName));
        LastName = Validation.RequireText(lastName, nameof(lastName));
        Email = Validation.RequireEmail(email);
        Age = age;
        Role = role;
        CreatedAt = DateTime.Now;
    }

    public void ChangeEmail(string email) => Email = Validation.RequireEmail(email);

    public override string ToString() => $"{Role}: {FirstName} {LastName} ({Email})";
}
