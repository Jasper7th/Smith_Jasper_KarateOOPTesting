namespace KarateSchoolSystem;

/// <summary>Junction object connecting a student to a karate class.</summary>
public sealed class Enrollment
{
    public int EnrollmentId { get; }
    public Student Student { get; }
    public KarateClass KarateClass { get; }
    public DateTime EnrollmentDate { get; }
    public RecordStatus Status { get; private set; }

    public Enrollment(int enrollmentId, Student student, KarateClass karateClass, DateTime enrollmentDate, RecordStatus status)
    {
        Validation.RequirePositive(enrollmentId, nameof(enrollmentId));
        Validation.RequireNotFuture(enrollmentDate, nameof(enrollmentDate));
        EnrollmentId = enrollmentId;
        Student = student ?? throw new ArgumentNullException(nameof(student));
        KarateClass = karateClass ?? throw new ArgumentNullException(nameof(karateClass));
        EnrollmentDate = enrollmentDate;
        Status = status;
    }

    public void Cancel() => Status = RecordStatus.Cancelled;

    public override string ToString() => $"Enrollment {EnrollmentId}: {Student.FirstName} in {KarateClass.ClassName} ({Status})";
}
