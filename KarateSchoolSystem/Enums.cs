namespace KarateSchoolSystem;

/// <summary>Valid user roles in the Karate School Management System.</summary>
public enum UserRole { Student, Instructor, Administrator }

/// <summary>Current status of a student, class enrollment, or instructor account.</summary>
public enum RecordStatus { Pending, Active, Inactive, Cancelled }

/// <summary>Supported attendance status values.</summary>
public enum AttendanceStatus { Present, Absent, Excused }

/// <summary>Supported payment status values.</summary>
public enum PaymentStatus { Pending, Completed, Failed }
