using StorageCleaner.Views;

namespace StorageCleaner.Actions;

/// <summary>
/// Base class for UI controls hosted inside ActionsView. Keeps dependencies minimal so
/// the WinForms designer can load without requiring XrmToolBox assemblies.
/// </summary>
public class ActionControlBase : ViewBase, IAction
{
    /// <summary>Optional hook so ActionsView can ask the control to persist changes.
    /// </summary>
    public virtual void SaveChanges()
    {
        // Default no-op
    }

    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string? Description { get; set; }
    public ActionControlBase Instance() => this;
}
