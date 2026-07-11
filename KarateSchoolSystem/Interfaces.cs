namespace KarateSchoolSystem;

/// <summary>Abstraction for objects that can produce a report summary.</summary>
public interface IReportable
{
    string GenerateSummary();
}

/// <summary>Abstraction for payment-capable records.</summary>
public interface IPayable
{
    PaymentStatus ProcessPayment(IPaymentStrategy strategy, Administrator administrator);
}

/// <summary>Abstraction for objects that can store attendance.</summary>
public interface IAttendanceTrackable
{
    void AddAttendance(Attendance attendance);
}

/// <summary>Strategy pattern interface for payment processing.</summary>
public interface IPaymentStrategy
{
    PaymentStatus Pay(decimal amount);
}
