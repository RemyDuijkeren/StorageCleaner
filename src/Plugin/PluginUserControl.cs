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
public class PluginUserControl : UserControl, IWorkerHost
{
    readonly Worker _worker = new();
    protected PluginControlBase PluginContext { get; private set; } = null!;
    protected IOrganizationService? Service => PluginContext?.Service;

    // Track only the current top-most ancestor so we can retry resolution when the root is reparented
    Control? _watchedRoot;

    public static bool IsDesignMode() => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        TryResolveContext();
    }

    protected override void OnParentChanged(EventArgs e)
    {
        base.OnParentChanged(e);
        TryResolveContext();
    }

    void TryResolveContext()
    {
        if (PluginContext != null || IsDesignMode()) return;

        // 1) Fast path: walk the current parent chain
        for (Control? c = this; c != null; c = c.Parent)
        {
            if (c is PluginControlBase pcb)
            {
                PluginContext = pcb;
                DetachRootWatcher();
                return;
            }
        }

        // 2) Not found yet: the ancestors might not be fully parented (e.g., TabControl without a parent yet).
        //    Watch only the current top-most ancestor (root) and retry when it gets attached higher up.
        var root = GetTopMostAncestor();
        if (root != null)
            AttachRootWatcher(root);
    }

    Control? GetTopMostAncestor()
    {
        Control? root = this;
        while (root?.Parent != null)
            root = root.Parent;
        return root;
    }

    void AttachRootWatcher(Control root)
    {
        if (ReferenceEquals(_watchedRoot, root)) return;
        DetachRootWatcher();
        _watchedRoot = root;
        _watchedRoot.ParentChanged += Root_ParentChanged;
    }

    void DetachRootWatcher()
    {
        if (_watchedRoot != null)
        {
            _watchedRoot.ParentChanged -= Root_ParentChanged;
            _watchedRoot = null;
        }
    }

    void Root_ParentChanged(object? sender, EventArgs e) => TryResolveContext();

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
    protected void ExecuteMethod(Action action)
    {
        if (PluginContext == null) throw new NullReferenceException("Plugin host context not yet available (ensure the control is parented before calling ExecuteMethod)");

        PluginContext.ExecuteMethod(null, new ExternalMethodCallerInfo(action));
    }

    protected void EnsureConnectedThen(Action<IOrganizationService> work)
    {
        if (IsDesignMode()) return;
        if (PluginContext == null) throw new InvalidOperationException("Plugin host context not yet available");

        // Use ExecuteMethod to auto-connect if needed
        PluginContext.ExecuteMethod(null, new ExternalMethodCallerInfo(() =>
        {
            var svc = PluginContext.Service;
            if (svc == null) throw new InvalidOperationException("Service unavailable after connect attempt");
            work(svc);
        }));
    }

    protected void EnsureConnectedThenAsync(Func<IOrganizationService, Task> work)
    {
        if (IsDesignMode()) return;
        if (PluginContext == null) throw new InvalidOperationException("Plugin host context not yet available");

        WorkAsync(new WorkAsyncInfo
        {
            Message = "Connecting...",
            Work = (bw, e) =>
            {
                PluginContext.ExecuteMethod(null, new ExternalMethodCallerInfo(() =>
                {
                    e.Result = PluginContext.Service ?? throw new InvalidOperationException("Service unavailable after connect attempt");
                }));
            },
            PostWorkCallBack = e =>
            {
                var svc = (IOrganizationService)e.Result!;
                _ = work(svc); // you can await with async void wrapper if needed
            }
        });
    }
}
