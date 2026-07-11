namespace KarateSchoolSystem;

/// <summary>Stores and processes a student payment transaction.</summary>
public sealed class Payment : IPayable
{
    public int PaymentId { get; }
    public Student Student { get; }
    public decimal Amount { get; }
    public DateTime PaymentDate { get; }
    public string PaymentMethod { get; }
    public PaymentStatus Status { get; private set; }
    public Administrator? ProcessedBy { get; private set; }

    public Payment(int paymentId, Student student, decimal amount, DateTime paymentDate, string paymentMethod)
    {
        Validation.RequirePositive(paymentId, nameof(paymentId));
        if (amount <= 0) throw new ArgumentException("Payment amount must be greater than zero.", nameof(amount));
        Validation.RequireNotFuture(paymentDate, nameof(paymentDate));
        PaymentId = paymentId;
        Student = student ?? throw new ArgumentNullException(nameof(student));
        Amount = amount;
        PaymentDate = paymentDate;
        PaymentMethod = Validation.RequireText(paymentMethod, nameof(paymentMethod));
        Status = PaymentStatus.Pending;
    }

    public PaymentStatus ProcessPayment(IPaymentStrategy strategy, Administrator administrator)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        ProcessedBy = administrator ?? throw new ArgumentNullException(nameof(administrator));
        Status = strategy.Pay(Amount);
        return Status;
    }

    public void MarkFailed() => Status = PaymentStatus.Failed;

    public override string ToString() => $"Payment {PaymentId}: {Amount:C} by {Student.FirstName} ({Status})";
}
