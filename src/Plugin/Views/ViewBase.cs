using System.ComponentModel;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace StorageCleaner.Views;

/// <summary>
/// Lightweight base class for all in-plugin views. Keeps XrmToolBox context without
/// requiring the view to inherit from PluginControlBase (only the main control should).
/// Provides optional delegation to the host PluginControlBase so subviews can still
/// use WorkAsync, ExecuteMethod and logging without tight inheritance.
/// </summary>
public class ViewBase : UserControl
{
    /// <summary>Reference to the hosting PluginControlBase (MainControl).</summary>
    protected PluginControlBase Host { get; private set; } = null!;

    /// <summary>Single initialization entry point for subviews.</summary>
    public void Initialize(PluginControlBase host)
    {
        Host = host;
    }

    public static bool IsDesignMode() => LicenseManager.UsageMode == LicenseUsageMode.Designtime;
}
