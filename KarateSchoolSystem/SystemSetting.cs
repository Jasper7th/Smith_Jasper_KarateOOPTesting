namespace KarateSchoolSystem;

/// <summary>Stores configurable system options and business rules.</summary>
public sealed class SystemSetting
{
    public int SettingId { get; }
    public string SettingName { get; }
    public string SettingValue { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Administrator UpdatedBy { get; private set; }

    public SystemSetting(int settingId, string settingName, string settingValue, Administrator updatedBy)
    {
        Validation.RequirePositive(settingId, nameof(settingId));
        SettingId = settingId;
        SettingName = Validation.RequireText(settingName, nameof(settingName));
        UpdatedBy = updatedBy ?? throw new ArgumentNullException(nameof(updatedBy));
        SettingValue = Validation.RequireText(settingValue, nameof(settingValue));
        UpdatedAt = DateTime.Now;
    }

    public void UpdateValue(string newValue, Administrator updatedBy)
    {
        UpdatedBy = updatedBy ?? throw new ArgumentNullException(nameof(updatedBy));
        SettingValue = Validation.RequireText(newValue, nameof(newValue));
        UpdatedAt = DateTime.Now;
    }

    public override string ToString() => $"Setting {SettingName} = {SettingValue}";
}
