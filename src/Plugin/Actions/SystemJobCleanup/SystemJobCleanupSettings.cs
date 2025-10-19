namespace StorageCleaner.Actions.SystemJobCleanup;

/// <summary>
/// Plain settings object for System Job Cleanup feature.
/// Defaults reflect Microsoft documented defaults.
/// </summary>
public class SystemJobCleanupSettings
{
    public bool Enable { get; set; } = true;
    public int SucceededDays { get; set; } = 30; // 0-90
    public int CanceledDays { get; set; } = 60;   // 0-180
    public int FailedDays { get; set; } = 60;     // 0-180
}
