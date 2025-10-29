namespace StorageCleaner.Actions.SystemJobCleanup;

/// <summary>Configuration for cleaning up system jobs within an organization, including retention policies for succeeded, canceled, and failed system jobs.</summary>
public class SystemJobCleanupFeature
{
    // Backing fields
    bool _enableSystemJobCleanup = true;
    int _succeededDays = 30; // 0-90
    int _canceledDays = 60;  // 0-180
    int _failedDays = 60;    // 0-180

    // Baseline snapshot loaded from Dataverse to detect changes
    bool _baselineEnableSystemJobCleanup;
    int _baselineSucceededDays;
    int _baselineCanceledDays;
    int _baselineFailedDays;

    /// <summary>Indicates that one or more properties differ from the loaded baseline.</summary>
    public bool IsDirty => _enableSystemJobCleanup != _baselineEnableSystemJobCleanup
    || _succeededDays != _baselineSucceededDays
    || _canceledDays != _baselineCanceledDays
    || _failedDays != _baselineFailedDays;

    public bool EnableSystemJobCleanup
    {
        get => _enableSystemJobCleanup;
        set { _enableSystemJobCleanup = value; }
    }

    public int SucceededSystemJobPersistenceInDays
    {
        get => _succeededDays;
        set { _succeededDays = value; }
    }

    public int CanceledSystemJobPersistenceInDays
    {
        get => _canceledDays;
        set { _canceledDays = value; }
    }

    public int FailedSystemJobPersistenceInDays
    {
        get => _failedDays;
        set { _failedDays = value; }
    }

    // Policy constants exposed for UI bounds and consumers
    public int MaxPersistenceDaysForSucceededSystemJobs => 90;
    public int MaxPersistenceDaysForCanceledOrFailedSystemJobs => 180;

    /// <summary>
    /// Capture the current values as the loaded baseline and reset IsDirty=false.
    /// Call this after loading from Dataverse.
    /// </summary>
    public void MarkLoaded()
    {
        _baselineEnableSystemJobCleanup = _enableSystemJobCleanup;
        _baselineSucceededDays = _succeededDays;
        _baselineCanceledDays = _canceledDays;
        _baselineFailedDays = _failedDays;
    }

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
