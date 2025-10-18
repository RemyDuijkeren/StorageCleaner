using System;

namespace DataverseStorageCleaner.Views.Actions;

public class CleanUpSystemJobsAction : IAction
{
    public string Id => "CleanUpSystemJobs";
    public string Name => "Clean Up System Jobs";
    public string? Description => "Automatic deletion of System Jobs";

    public ActionControlBase CreateControl()
    {
        return new CleanUpSystemJobsActionControl();
    }
}
