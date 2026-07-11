namespace KarateSchoolSystem;

/// <summary>Tracks belt progress and promotion recommendations.</summary>
public sealed class BeltProgress
{
    public int ProgressId { get; }
    public Student Student { get; }
    public Instructor EvaluatedBy { get; }
    public string CurrentBelt { get; }
    public string TargetBelt { get; }
    public DateTime EvaluationDate { get; }
    public bool PromotionRecommended { get; }

    public BeltProgress(Student student, Instructor evaluatedBy, string currentBelt, string targetBelt, DateTime evaluationDate, bool promotionRecommended, int progressId = 1)
    {
        Validation.RequirePositive(progressId, nameof(progressId));
        Validation.RequireNotFuture(evaluationDate, nameof(evaluationDate));
        ProgressId = progressId;
        Student = student ?? throw new ArgumentNullException(nameof(student));
        EvaluatedBy = evaluatedBy ?? throw new ArgumentNullException(nameof(evaluatedBy));
        CurrentBelt = Validation.RequireText(currentBelt, nameof(currentBelt));
        TargetBelt = Validation.RequireText(targetBelt, nameof(targetBelt));
        PromotionRecommended = promotionRecommended;
        EvaluationDate = evaluationDate;
    }

    public override string ToString() => $"Belt progress for {Student.FirstName}: {CurrentBelt} to {TargetBelt}, Recommended: {PromotionRecommended}";
}
