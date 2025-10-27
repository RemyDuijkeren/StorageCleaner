using System.Reflection;
using StorageCleaner.Actions;

namespace StorageCleaner.Views;

public partial class ActionsView : PluginUserControl
{
    record ActionItem(Type Type, string Id, string DisplayName, string Description)
    {
        public override string ToString() => DisplayName;
    }

    readonly List<ActionItem> _items = new();
    ActionControl? _current;
    bool _actionsLoaded;

    public ActionsView()
    {
        InitializeComponent();
        // Defer loading actions until control is loaded to ensure Handle created
        //this.Load += ActionsView_Load;
        SafeLoadActions();
    }


    void ActionsView_Load(object? sender, EventArgs e)
    {
        if (PluginContext == null)
            return;
        SafeLoadActions();

        // // If the control is already created and visible, load now; otherwise Load event will handle it
        // if (IsHandleCreated)
        // {
        //     SafeLoadActions();
        // }
    }

    void SafeLoadActions()
    {
        if (_actionsLoaded)
            return;
        try
        {
            LoadActions();
        }
        catch (Exception ex)
        {
            if (PluginContext != null)
                PluginContext.ShowErrorDialog(ex, "Failed to load actions");
            else
                MessageBox.Show($"Failed to load actions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    void LoadActions()
    {
        _items.Clear();

        foreach ((Type type, ActionAttribute attribute) in FindActions(Assembly.GetExecutingAssembly()))
        {
            _items.Add(new ActionItem(type, attribute.Id, attribute.DisplayName, attribute.Description));
        }

        // Temporarily detach to avoid duplicate reloads during rebinding
        lstActions.SelectedIndexChanged -= lstActions_SelectedIndexChanged;

        lstActions.DataSource = null;
        lstActions.DisplayMember = nameof(ActionItem.DisplayName);
        lstActions.ValueMember = nameof(ActionItem.Id);
        lstActions.DataSource = _items;

        if (_items.Count > 0)
        {
            lstActions.SelectedIndex = 0;
            // Ensure first item loads even if SelectedIndexChanged isn't raised by data binding
            ShowSelectedAction();
        }

        // Reattach event after initial load
        lstActions.SelectedIndexChanged += lstActions_SelectedIndexChanged;

        _actionsLoaded = true;
    }

    void lstActions_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ShowSelectedAction();
    }

    void ShowSelectedAction()
    {
        if (lstActions.SelectedItem is not ActionItem item)
            return;

        // If the currently displayed control is already the same type, do nothing to preserve state
        if (_current != null && _current.GetType() == item.Type)
            return;

        try
        {
            if (Activator.CreateInstance(item.Type) is not ActionControl ctrl)
                throw new InvalidOperationException($"Could not create action control: {item.DisplayName}");

            ctrl.Dock = DockStyle.Fill;

            pnlHost.SuspendLayout();
            pnlHost.Controls.Clear();
            pnlHost.Controls.Add(ctrl);
            pnlHost.ResumeLayout();

            _current = ctrl;
        }
        catch (Exception ex)
        {
            PluginContext.ShowErrorDialog(ex, $"Failed to load action '{item.DisplayName}'");
        }
    }

    /// <summary>
    /// Finds and returns all types within the specified assembly that are concrete, derive from
    /// <see cref="ActionControl"/>, have a public default constructor, and are decorated with
    /// the <see cref="ActionAttribute"/>. The results are ordered by the <c>DisplayName</c> property
    /// of the <see cref="ActionAttribute"/>.
    /// </summary>
    /// <param name="assembly">The assembly to search for types that meet the specified criteria.</param>
    /// <returns>
    /// A collection of tuples where each tuple contains the following:
    /// - <c>Type</c>: The type of the class derived from <see cref="ActionControl"/>.
    /// - <c>Action</c>: The associated <see cref="ActionAttribute"/> metadata of the class.
    /// </returns>
    static IEnumerable<(Type Type, ActionAttribute Action)> FindActions(Assembly assembly) =>
        assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => typeof(ActionControl).IsAssignableFrom(t))
                .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => new
                {
                    Type = t,
                    Meta = t.GetCustomAttributes(typeof(ActionAttribute), inherit: false)
                            .OfType<ActionAttribute>()
                            .FirstOrDefault()
                })
                .Where(x => x.Meta != null)
                .OrderBy(x => x.Meta!.DisplayName)
                .Select(x => (x.Type, x.Meta!));
}
