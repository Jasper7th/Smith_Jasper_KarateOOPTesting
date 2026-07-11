namespace KarateSchoolSystem;

/// <summary>Stores attendance for a student in a class on a specific date.</summary>
public sealed class Attendance
{
    public int AttendanceId { get; }
    public Student Student { get; }
    public KarateClass KarateClass { get; }
    public Instructor RecordedBy { get; }
    public DateTime AttendanceDate { get; }
    public AttendanceStatus Status { get; }

    public Attendance(Student student, KarateClass karateClass, Instructor recordedBy, DateTime attendanceDate, AttendanceStatus status, int attendanceId = 1)
    {
        Validation.RequirePositive(attendanceId, nameof(attendanceId));
        Validation.RequireNotFuture(attendanceDate, nameof(attendanceDate));
        Student = student ?? throw new ArgumentNullException(nameof(student));
        KarateClass = karateClass ?? throw new ArgumentNullException(nameof(karateClass));
        RecordedBy = recordedBy ?? throw new ArgumentNullException(nameof(recordedBy));
        AttendanceDate = attendanceDate;
        Status = status;
    }

    public override string ToString() => $"Attendance: {Student.FirstName} {Student.LastName} was {Status} on {AttendanceDate:d}";
}
