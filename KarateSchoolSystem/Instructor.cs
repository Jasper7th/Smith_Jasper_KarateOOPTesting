namespace KarateSchoolSystem;

/// <summary>Represents an instructor responsible for teaching and evaluating students.</summary>
public sealed class Instructor : User
{
    public string Specialty { get; private set; }
    public string Rank { get; private set; }

    public Instructor(int id, string firstName, string lastName, string email, int age, string rank, string specialty)
        : base(id, firstName, lastName, email, age, UserRole.Instructor)
    {
        Rank = Validation.RequireText(rank, nameof(rank));
        Specialty = Validation.RequireText(specialty, nameof(specialty));
    }

    public Attendance RecordAttendance(Student student, KarateClass karateClass, DateTime date, AttendanceStatus status)
    {
        return new Attendance(student, karateClass, this, date, status);
    }

    public BeltProgress EvaluateProgress(Student student, string targetBelt, bool promotionRecommended)
    {
        return new BeltProgress(student, this, student.BeltLevel, targetBelt, DateTime.Today, promotionRecommended);
    }

    public override string ToString() => $"Instructor {FirstName} {LastName}, Rank: {Rank}, Specialty: {Specialty}";
}
