namespace KarateSchoolSystem;

/// <summary>Represents a student enrolled in the karate academy.</summary>
public sealed class Student : User, IAttendanceTrackable
{
    private static readonly HashSet<string> ValidBelts = new(StringComparer.OrdinalIgnoreCase)
    { "White", "Yellow", "Orange", "Green", "Blue", "Purple", "Brown", "Black" };

    private readonly List<Enrollment> _enrollments = new();
    private readonly List<Attendance> _attendance = new();

    public string BeltLevel { get; private set; }
    public DateTime EnrollmentDate { get; }
    public IReadOnlyList<Enrollment> Enrollments => _enrollments.AsReadOnly();
    public IReadOnlyList<Attendance> AttendanceRecords => _attendance.AsReadOnly();

    public Student(int id, string firstName, string lastName, string email, int age, string beltLevel)
        : base(id, firstName, lastName, email, age, UserRole.Student)
    {
        SetBeltLevel(beltLevel);
        EnrollmentDate = DateTime.Today;
    }

    public void SetBeltLevel(string beltLevel)
    {
        string value = Validation.RequireText(beltLevel, nameof(beltLevel));
        if (!ValidBelts.Contains(value)) throw new ArgumentException("Belt level is invalid.", nameof(beltLevel));
        BeltLevel = value;
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        ArgumentNullException.ThrowIfNull(enrollment);
        if (enrollment.Student.UserId != UserId) throw new InvalidOperationException("Enrollment belongs to a different student.");
        if (_enrollments.Any(e => e.KarateClass.ClassId == enrollment.KarateClass.ClassId))
            throw new InvalidOperationException("Student is already enrolled in this class.");
        _enrollments.Add(enrollment);
    }

    public void AddAttendance(Attendance attendance)
    {
        ArgumentNullException.ThrowIfNull(attendance);
        if (attendance.Student.UserId != UserId) throw new InvalidOperationException("Attendance belongs to a different student.");
        _attendance.Add(attendance);
    }

    public override string ToString() => $"Student {FirstName} {LastName}, Belt: {BeltLevel}";
}
