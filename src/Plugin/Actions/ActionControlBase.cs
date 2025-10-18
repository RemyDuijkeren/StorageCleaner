using StorageCleaner.Views;

namespace StorageCleaner.Actions;

/// <summary>
/// Base class for UI controls hosted inside ActionsView. Keeps dependencies minimal so
/// the WinForms designer can load without requiring XrmToolBox assemblies.
/// </summary>
public class ActionControlBase : ViewBase
{
    /// <summary>Optional hook so ActionsView can ask the control to persist changes.
    /// </summary>
    public virtual void SaveChanges()
    {
        // Default no-op
    }
}
