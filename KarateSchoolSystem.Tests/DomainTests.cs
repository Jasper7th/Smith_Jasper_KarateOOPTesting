using KarateSchoolSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KarateSchoolSystem.Tests;

[TestClass]
public class DomainTests
{
    private static Student Student() => new(1, "Anna", "Lee", "anna@email.com", 22, "White");
    private static Instructor Instructor() => new(2, "Ken", "Miles", "ken@email.com", 35, "Black", "Kata");
    private static Administrator Admin(string access = "Full") => new(3, "Mia", "Stone", "mia@email.com", 40, access);
    private static KarateClass Class() => new(10, "Beginner Karate", "White", 2, DayOfWeek.Monday, new TimeSpan(18, 0, 0), new TimeSpan(19, 0, 0), Instructor());

    [TestMethod]
    public void StudentConstructor_ValidData_CreatesStudent()
    {
        Student student = Student();
        Assert.AreEqual("White", student.BeltLevel);
        Assert.AreEqual(UserRole.Student, student.Role);
        Assert.IsTrue(student.ToString().Contains("Student"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void StudentConstructor_EmptyBeltLevel_ThrowsException()
    {
        _ = new Student(1, "Anna", "Lee", "anna@email.com", 22, "");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void StudentConstructor_InvalidBeltLevel_ThrowsException()
    {
        _ = new Student(1, "Anna", "Lee", "anna@email.com", 22, "Gold");
    }

    [TestMethod]
    public void UserValidation_ChangeEmail_Works()
    {
        Student student = Student();
        student.ChangeEmail("new@email.com");
        Assert.AreEqual("new@email.com", student.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UserConstructor_InvalidEmail_ThrowsException()
    {
        _ = new Student(1, "Anna", "Lee", "bad-email", 22, "White");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UserConstructor_TooYoung_ThrowsException()
    {
        _ = new Student(1, "Anna", "Lee", "anna@email.com", 15, "White");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UserConstructor_EmptyName_ThrowsException()
    {
        _ = new Student(1, "", "Lee", "anna@email.com", 22, "White");
    }

    [TestMethod]
    public void Instructor_RecordAttendance_CreatesAttendance()
    {
        Instructor instructor = Instructor();
        Attendance attendance = instructor.RecordAttendance(Student(), Class(), DateTime.Today, AttendanceStatus.Present);
        Assert.AreEqual(AttendanceStatus.Present, attendance.Status);
        Assert.IsTrue(attendance.ToString().Contains("Present"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Attendance_FutureDate_ThrowsException()
    {
        _ = new Attendance(Student(), Class(), Instructor(), DateTime.Today.AddDays(1), AttendanceStatus.Absent);
    }

    [TestMethod]
    public void Instructor_EvaluateProgress_ReturnsBeltProgress()
    {
        BeltProgress progress = Instructor().EvaluateProgress(Student(), "Yellow", true);
        Assert.AreEqual("Yellow", progress.TargetBelt);
        Assert.IsTrue(progress.PromotionRecommended);
        Assert.IsTrue(progress.ToString().Contains("Recommended"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void BeltProgress_EmptyTargetBelt_ThrowsException()
    {
        _ = new BeltProgress(Student(), Instructor(), "White", "", DateTime.Today, false);
    }

    [TestMethod]
    public void KarateClass_EnrollStudent_CreatesEnrollment()
    {
        KarateClass karateClass = Class();
        Student student = Student();
        Enrollment enrollment = karateClass.EnrollStudent(student);
        Assert.AreEqual(RecordStatus.Active, enrollment.Status);
        Assert.AreEqual(1, student.Enrollments.Count);
        Assert.IsTrue(enrollment.ToString().Contains("Enrollment"));
        Assert.IsTrue(karateClass.ToString().Contains("Beginner"));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void KarateClass_DuplicateStudent_ThrowsException()
    {
        KarateClass karateClass = Class();
        Student student = Student();
        karateClass.EnrollStudent(student);
        karateClass.EnrollStudent(student);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void KarateClass_CapacityExceeded_ThrowsException()
    {
        KarateClass karateClass = new(10, "Small Class", "White", 1, DayOfWeek.Monday, new TimeSpan(18, 0, 0), new TimeSpan(19, 0, 0), Instructor());
        karateClass.EnrollStudent(Student());
        karateClass.EnrollStudent(new Student(4, "Ben", "Ray", "ben@email.com", 20, "White"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void KarateClass_InvalidTime_ThrowsException()
    {
        _ = new KarateClass(10, "Bad", "White", 1, DayOfWeek.Monday, new TimeSpan(19, 0, 0), new TimeSpan(18, 0, 0), Instructor());
    }

    [TestMethod]
    public void Enrollment_Cancel_ChangesStatus()
    {
        Enrollment enrollment = new(1, Student(), Class(), DateTime.Today, RecordStatus.Active);
        enrollment.Cancel();
        Assert.AreEqual(RecordStatus.Cancelled, enrollment.Status);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Enrollment_FutureDate_ThrowsException()
    {
        _ = new Enrollment(1, Student(), Class(), DateTime.Today.AddDays(1), RecordStatus.Active);
    }

    [TestMethod]
    public void Payment_ProcessCash_CompletesPayment()
    {
        Payment payment = new(1, Student(), 99.95m, DateTime.Today, "Cash");
        PaymentStatus status = Admin().ProcessPayment(payment, new CashPaymentStrategy());
        Assert.AreEqual(PaymentStatus.Completed, status);
        Assert.IsTrue(payment.ToString().Contains("Payment"));
        Assert.IsNotNull(payment.ProcessedBy);
    }

    [TestMethod]
    public void Payment_ProcessCard_CompletesPayment()
    {
        Payment payment = new(1, Student(), 99.95m, DateTime.Today, "Card");
        PaymentStatus status = payment.ProcessPayment(new CardPaymentStrategy("1234"), Admin());
        Assert.AreEqual(PaymentStatus.Completed, status);
        Assert.IsTrue(new CardPaymentStrategy("1234").ToString().Contains("1234"));
    }

    [TestMethod]
    public void Payment_MarkFailed_ChangesStatus()
    {
        Payment payment = new(1, Student(), 99.95m, DateTime.Today, "Cash");
        payment.MarkFailed();
        Assert.AreEqual(PaymentStatus.Failed, payment.Status);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Payment_NegativeAmount_ThrowsException()
    {
        _ = new Payment(1, Student(), -1m, DateTime.Today, "Cash");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CardPaymentStrategy_InvalidCard_ThrowsException()
    {
        _ = new CardPaymentStrategy("12A4");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CashPaymentStrategy_InvalidAmount_ThrowsException()
    {
        _ = new CashPaymentStrategy().Pay(0m);
    }

    [TestMethod]
    public void Administrator_ConfigureSetting_UpdatesValue()
    {
        Administrator admin = Admin();
        SystemSetting setting = new(1, "MaxClassSize", "20", admin);
        admin.ConfigureSetting(setting, "25");
        Assert.AreEqual("25", setting.SettingValue);
        Assert.IsTrue(setting.ToString().Contains("MaxClassSize"));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Administrator_UnauthorizedSettingChange_ThrowsException()
    {
        Administrator admin = Admin("ReadOnly");
        SystemSetting setting = new(1, "MaxClassSize", "20", Admin());
        admin.ConfigureSetting(setting, "25");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SystemSetting_EmptyValue_ThrowsException()
    {
        _ = new SystemSetting(1, "MaxClassSize", "", Admin());
    }

    [TestMethod]
    public void Announcement_ValidData_CreatesAnnouncement()
    {
        Announcement announcement = new(1, "Closed", "No class Friday", "All", Admin());
        Assert.IsTrue(announcement.ToString().Contains("Closed"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Announcement_EmptyTitle_ThrowsException()
    {
        _ = new Announcement(1, "", "No class", "All", Admin());
    }

    [TestMethod]
    public void Report_GenerateSummary_ReturnsText()
    {
        Report report = new(1, "Attendance", Admin());
        Assert.IsTrue(report.GenerateSummary().Contains("Attendance"));
        Assert.AreEqual(report.GenerateSummary(), report.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Report_EmptyType_ThrowsException()
    {
        _ = new Report(1, "", Admin());
    }

    [TestMethod]
    public void UserFactory_CreatesAllUserTypes()
    {
        User student = UserFactory.CreateUser(UserRole.Student, 1, "A", "B", "a@b.com", 20, "White");
        User instructor = UserFactory.CreateUser(UserRole.Instructor, 2, "C", "D", "c@d.com", 30, "Black");
        User admin = UserFactory.CreateUser(UserRole.Administrator, 3, "E", "F", "e@f.com", 40, "Full");
        Assert.IsInstanceOfType(student, typeof(Student));
        Assert.IsInstanceOfType(instructor, typeof(Instructor));
        Assert.IsInstanceOfType(admin, typeof(Administrator));
    }

    [TestMethod]
    public void Polymorphism_UserToString_IsRoleSpecific()
    {
        User[] users = { Student(), Instructor(), Admin() };
        Assert.IsTrue(users[0].ToString().Contains("Student"));
        Assert.IsTrue(users[1].ToString().Contains("Instructor"));
        Assert.IsTrue(users[2].ToString().Contains("Administrator"));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Student_AddEnrollmentForDifferentStudent_ThrowsException()
    {
        Student anna = Student();
        Student ben = new(5, "Ben", "Ray", "ben@email.com", 21, "White");
        Enrollment enrollment = new(1, ben, Class(), DateTime.Today, RecordStatus.Active);
        anna.AddEnrollment(enrollment);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Student_AddAttendanceForDifferentStudent_ThrowsException()
    {
        Student anna = Student();
        Student ben = new(5, "Ben", "Ray", "ben@email.com", 21, "White");
        Attendance attendance = new(ben, Class(), Instructor(), DateTime.Today, AttendanceStatus.Present);
        anna.AddAttendance(attendance);
    }
}
