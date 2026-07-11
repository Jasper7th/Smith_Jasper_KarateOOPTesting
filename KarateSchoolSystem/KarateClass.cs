namespace KarateSchoolSystem;

/// <summary>Represents a scheduled karate class.</summary>
public sealed class KarateClass
{
    private readonly List<Enrollment> _enrollments = new();

    public int ClassId { get; }
    public string ClassName { get; private set; }
    public string Level { get; private set; }
    public int Capacity { get; private set; }
    public DayOfWeek DayOfWeek { get; }
    public TimeSpan StartTime { get; }
    public TimeSpan EndTime { get; }
    public Instructor Instructor { get; }
    public IReadOnlyList<Enrollment> Enrollments => _enrollments.AsReadOnly();

    public KarateClass(int classId, string className, string level, int capacity, DayOfWeek dayOfWeek,
        TimeSpan startTime, TimeSpan endTime, Instructor instructor)
    {
        Validation.RequirePositive(classId, nameof(classId));
        if (capacity <= 0) throw new ArgumentException("Class capacity must be greater than zero.", nameof(capacity));
        if (endTime <= startTime) throw new ArgumentException("End time must be after start time.", nameof(endTime));
        ClassId = classId;
        ClassName = Validation.RequireText(className, nameof(className));
        Level = Validation.RequireText(level, nameof(level));
        Capacity = capacity;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        Instructor = instructor ?? throw new ArgumentNullException(nameof(instructor));
    }

    public Enrollment EnrollStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        if (_enrollments.Count >= Capacity) throw new InvalidOperationException("Class capacity exceeded.");
        if (_enrollments.Any(e => e.Student.UserId == student.UserId)) throw new InvalidOperationException("Duplicate student ID.");
        var enrollment = new Enrollment(_enrollments.Count + 1, student, this, DateTime.Today, RecordStatus.Active);
        _enrollments.Add(enrollment);
        student.AddEnrollment(enrollment);
        return enrollment;
    }

    public override string ToString() => $"Class {ClassName} ({Level}) on {DayOfWeek} at {StartTime}";
}
