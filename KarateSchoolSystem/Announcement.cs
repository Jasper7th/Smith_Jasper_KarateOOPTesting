namespace KarateSchoolSystem;

/// <summary>Stores a school announcement created by an administrator.</summary>
public sealed class Announcement
{
    public int AnnouncementId { get; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; }
    public string Audience { get; }
    public Administrator CreatedBy { get; }

    public Announcement(int announcementId, string title, string message, string audience, Administrator createdBy)
    {
        Validation.RequirePositive(announcementId, nameof(announcementId));
        AnnouncementId = announcementId;
        Title = Validation.RequireText(title, nameof(title));
        Message = Validation.RequireText(message, nameof(message));
        Audience = Validation.RequireText(audience, nameof(audience));
        CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        CreatedAt = DateTime.Now;
    }

    public override string ToString() => $"Announcement: {Title} for {Audience}";
}
