namespace KarateSchoolSystem;

/// <summary>Represents an administrative user who manages school operations.</summary>
public sealed class Administrator : User
{
    public string PermissionLevel { get; private set; }

    public Administrator(int id, string firstName, string lastName, string email, int age, string permissionLevel)
        : base(id, firstName, lastName, email, age, UserRole.Administrator)
    {
        PermissionLevel = Validation.RequireText(permissionLevel, nameof(permissionLevel));
    }

    public void EnsureCanConfigureSettings()
    {
        if (!PermissionLevel.Equals("Full", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Administrator is not authorized to configure settings.");
    }

    public void ConfigureSetting(SystemSetting setting, string newValue)
    {
        EnsureCanConfigureSettings();
        ArgumentNullException.ThrowIfNull(setting);
        setting.UpdateValue(newValue, this);
    }

    public PaymentStatus ProcessPayment(Payment payment, IPaymentStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(payment);
        return payment.ProcessPayment(strategy, this);
    }

    public override string ToString() => $"Administrator {FirstName} {LastName}, Access: {PermissionLevel}";
}
