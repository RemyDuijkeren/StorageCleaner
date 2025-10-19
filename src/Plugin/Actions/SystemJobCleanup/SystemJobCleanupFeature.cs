namespace StorageCleaner.Actions.SystemJobCleanup;

/// <summary>Configuration for cleaning up system jobs within an organization, including retention policies for succeeded, canceled, and failed system jobs.</summary>
public class SystemJobCleanupFeature
{
    public bool EnableSystemJobCleanup { get; set; } = true;
    public int SucceededSystemJobPersistenceInDays { get; set; } = 30; // 0-90
    public int CanceledSystemJobPersistenceInDays { get; set; } = 60;   // 0-180
    public int FailedSystemJobPersistenceInDays { get; set; } = 60;     // 0-180

    // Policy constants exposed for UI bounds and consumers
    public int MaxPersistenceDaysForSucceededSystemJobs { get; } = 90;
    public int MaxPersistenceDaysForCanceledOrFailedSystemJobs { get; } = 180;

    // Validation centralizes policy enforcement in the domain type
    public (bool ok, string? error) Validate()
    {
        if (SucceededSystemJobPersistenceInDays < 0 || SucceededSystemJobPersistenceInDays > MaxPersistenceDaysForSucceededSystemJobs)
            return (false, $"SucceededDays must be between 0 and {MaxPersistenceDaysForSucceededSystemJobs}.");
        if (CanceledSystemJobPersistenceInDays < 0 || CanceledSystemJobPersistenceInDays > MaxPersistenceDaysForCanceledOrFailedSystemJobs)
            return (false, $"CanceledDays must be between 0 and {MaxPersistenceDaysForCanceledOrFailedSystemJobs}.");
        if (FailedSystemJobPersistenceInDays < 0 || FailedSystemJobPersistenceInDays > MaxPersistenceDaysForCanceledOrFailedSystemJobs)
            return (false, $"FailedDays must be between 0 and {MaxPersistenceDaysForCanceledOrFailedSystemJobs}.");
        return (true, null);
    }

    // Optional clamp helpers for UI-friendly coercion
    public int ClampSucceeded(int value) => Clamp(value, 0, MaxPersistenceDaysForSucceededSystemJobs);
    public int ClampCanceled(int value) => Clamp(value, 0, MaxPersistenceDaysForCanceledOrFailedSystemJobs);
    public int ClampFailed(int value) => Clamp(value, 0, MaxPersistenceDaysForCanceledOrFailedSystemJobs);

    static int Clamp(int value, int min, int max) => value < min ? min : (value > max ? max : value);
}
