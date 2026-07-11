namespace KarateSchoolSystem;

/// <summary>Cash payment processing strategy.</summary>
public sealed class CashPaymentStrategy : IPaymentStrategy
{
    public PaymentStatus Pay(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Payment amount must be greater than zero.", nameof(amount));
        return PaymentStatus.Completed;
    }

    public override string ToString() => "Cash payment strategy";
}

/// <summary>Card payment processing strategy with simple authorization simulation.</summary>
public sealed class CardPaymentStrategy : IPaymentStrategy
{
    public string LastFourDigits { get; }

    public CardPaymentStrategy(string lastFourDigits)
    {
        LastFourDigits = Validation.RequireText(lastFourDigits, nameof(lastFourDigits));
        if (LastFourDigits.Length != 4 || !LastFourDigits.All(char.IsDigit))
            throw new ArgumentException("Card last four digits must contain exactly four numbers.", nameof(lastFourDigits));
    }

    public PaymentStatus Pay(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Payment amount must be greater than zero.", nameof(amount));
        return PaymentStatus.Completed;
    }

    public override string ToString() => $"Card payment strategy ending in {LastFourDigits}";
}
