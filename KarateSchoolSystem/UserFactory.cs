namespace KarateSchoolSystem;

/// <summary>Factory Method pattern for creating user subtypes.</summary>
public static class UserFactory
{
    public static User CreateUser(UserRole role, int id, string firstName, string lastName, string email, int age, string roleSpecificValue)
    {
        return role switch
        {
            UserRole.Student => new Student(id, firstName, lastName, email, age, roleSpecificValue),
            UserRole.Instructor => new Instructor(id, firstName, lastName, email, age, roleSpecificValue, "General Karate"),
            UserRole.Administrator => new Administrator(id, firstName, lastName, email, age, roleSpecificValue),
            _ => throw new ArgumentException("Unsupported user role.", nameof(role))
        };
    }
}
