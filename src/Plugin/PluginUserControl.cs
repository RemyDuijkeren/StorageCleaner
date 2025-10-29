using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace StorageCleaner;

// Here’s a concise checklist of lifecycle events you can subscribe to on a WinForms control, grouped roughly by when they occur:
//
// Construction/parenting/handle lifecycle
// - ParentChanged: when Parent is set/changed/cleared.
// - CreateControl/OnCreateControl: when the control is created and its handle is created.
// - HandleCreated / HandleDestroyed: when the native handle is created/destroyed.
// - ControlAdded / ControlRemoved: when child controls are added/removed (on containers).
// - VisibleChanged: visibility toggled (can happen before/after handle exists).
// - EnabledChanged: enabled state changed.
//
// Layout/size/position
// - DockChanged / AnchorChanged
// - LocationChanged / SizeChanged / ClientSizeChanged
// - Layout: when layout occurs (e.g., after Add/Remove/Resize).
// - Resize: when the control is resized.
//
// Display/paint
// - Paint: when the control needs repainting.
// - BackColorChanged / ForeColorChanged / FontChanged
//
// Focus/interaction
// - Enter / Leave
// - GotFocus / LostFocus
// - Click / DoubleClick / MouseEnter / MouseLeave / MouseMove / MouseDown / MouseUp
// - KeyDown / KeyPress / KeyUp
//
// Data/context-related (common patterns)
// - Load: typically for forms and user controls when they are loaded for the first time.
// - Disposed / Disposing: when being disposed.
// - BindingContextChanged: when binding context changes.
//
// Advice: retrieving the MainControl (the PluginControlBase host)
// - Best event to resolve the host: OnParentChanged plus OnCreateControl.
//   - OnParentChanged fires as soon as the control is added to a container and any time it’s reparented.
//   - OnCreateControl ensures the handle exists and often occurs soon after parenting.
// - Strategy:
//   - In OnParentChanged: walk up Parent chain to find the PluginControlBase host. If not found yet (e.g., still being composed), keep a lightweight watcher on the current root and retry on its ParentChanged.
//   - Also call the same resolving method from OnCreateControl to cover cases where handle creation happens after parenting.
// - Avoid relying solely on Load: Load may be too late in some container scenarios, and it won’t fire if the control is never shown. ParentChanged + CreateControl gives earlier and more reliable resolution.
// - Do not resolve in the constructor: the control isn’t parented yet, so the host won’t be discoverable.

/// <summary>Lightweight base class for all in-plugin user controls.</summary>
/// <remarks>
/// Keeps XrmToolBox context without requiring the view to inherit from PluginControlBase (only the main control should).
/// </remarks>
public class PluginUserControl : UserControl, IWorkerHost
{
    readonly Worker _worker = new();
    readonly Lazy<PluginControlBase> _pluginContext;
    protected PluginControlBase PluginContext => _pluginContext.Value;
    protected IOrganizationService? Service => PluginContext.Service;

    public static bool IsDesignMode() => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    public PluginUserControl()
    {
        _pluginContext = new Lazy<PluginControlBase>(ResolvePluginContext);
    }

    PluginControlBase ResolvePluginContext()
    {
        if (IsDesignMode())
            throw new InvalidOperationException("Plugin host context not available in design mode");

        for (Control? c = this; c != null; c = c.Parent)
        {
            if (c is PluginControlBase pcb)
                return pcb;
        }

        throw new NullReferenceException("Plugin host context not yet available. Ensure this control is parented under a PluginControlBase before accessing PluginContext.");
    }

    public void SetWorkingMessage(string message, int width = 340, int height = 100) =>
        _worker.SetWorkingMessage(this, message, width, height);

    public void WorkAsync(WorkAsyncInfo info)
    {
        info.Host = this;
        _worker.WorkAsync(info);
    }

    public void RaiseRequestConnectionEvent(RequestConnectionEventArgs args) => PluginContext.RaiseRequestConnectionEvent(args);

    /// <summary>Checks to make sure that the Plugin has an IOrganizationService Connection before calling the action.</summary>
    /// <param name="action"></param>
    protected void ExecuteMethod(Action action) => PluginContext.ExecuteMethod(null, new ExternalMethodCallerInfo(action));
}
