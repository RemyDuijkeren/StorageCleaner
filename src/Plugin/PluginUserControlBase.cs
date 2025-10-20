using System.ComponentModel;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace StorageCleaner;

/// <summary>
/// Lightweight base class for all in-plugin user controls. Keeps XrmToolBox context without
/// requiring the view to inherit from PluginControlBase (only the main control should).
/// Provides optional delegation to the host PluginControlBase so subviews can still
/// use WorkAsync, ExecuteMethod and logging without tight inheritance.
/// </summary>
public class PluginUserControlBase : UserControl, IWorkerHost
{
    readonly Worker _worker = new();
    protected PluginControlBase MainControl { get; private set; } = null!;
    protected IOrganizationService? Service => MainControl?.Service;

    public void Initialize(PluginControlBase mainControl) =>
        MainControl = mainControl ?? throw new ArgumentNullException(nameof(mainControl));

    public static bool IsDesignMode() => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    public void SetWorkingMessage(string message, int width = 340, int height = 100) =>
        _worker.SetWorkingMessage(this, message, width, height);

    public void WorkAsync(WorkAsyncInfo info)
    {
        info.Host = this;
        _worker.WorkAsync(info);
    }

    public void RaiseRequestConnectionEvent(RequestConnectionEventArgs args) => MainControl.RaiseRequestConnectionEvent(args);

    /// <summary>Checks to make sure that the Plugin has an IOrganizationService Connection before calling the action.</summary>
    /// <param name="action"></param>
    protected void ExecuteMethod(Action action)
    {
        if (MainControl == null) throw new NullReferenceException("Use the Initialize(PluginUserControlBase) before ExecuteMethod(Action)");

        MainControl.ExecuteMethod(null, new ExternalMethodCallerInfo(action));
    }
}
