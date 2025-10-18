using System.Windows.Forms;

namespace DataverseStorageCleaner.Views.Actions;

/// <summary>
/// Base class for UI controls hosted inside ActionsView. Keeps dependencies minimal so
/// the WinForms designer can load without requiring XrmToolBox assemblies.
/// </summary>
public class ActionControlBase : UserControl
{
    /// <summary>Reference to the hosting control (MainControl). Stored as object to avoid design-time load issues.</summary>
    protected object Host { get; private set; } = null!;
    protected Settings? Settings { get; private set; }

    public virtual void Initialize(object host, Settings settings)
    {
        Host = host;
        Settings = settings;
    }

    /// <summary>
    /// Optional hook so ActionsView can ask the control to persist changes.
    /// </summary>
    public virtual void SaveChanges()
    {
        // Default no-op
    }
}
