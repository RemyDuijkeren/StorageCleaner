namespace StorageCleaner.Actions;

/// <summary>
/// Base class for UI controls hosted inside ActionsView. Keeps dependencies minimal so
/// the WinForms designer can load without requiring XrmToolBox assemblies.
/// </summary>
public class ActionControl : PluginUserControlBase
{
    /// <summary>Optional hook so ActionsView can ask the control to load or analyze the current situation.
    /// </summary>
    public virtual void AnalyzeClick(object sender, EventArgs e)
    {
        // Default no-op
    }

    /// <summary>Optional hook so ActionsView can ask the control to persist changes.</summary>
    public virtual void CleanClick(object sender, EventArgs e)
    {
        // Default no-op
    }
}
