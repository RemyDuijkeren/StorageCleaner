using System.Reflection;
using StorageCleaner.Actions;

namespace StorageCleaner.Views;

public partial class ActionsView : ViewBase
{
    private sealed class ActionItem
    {
        public Type Type { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public override string ToString() => DisplayName;
    }

    private readonly List<ActionItem> _items = new();
    private ActionControl? _current;

    public ActionsView()
    {
        InitializeComponent();
        // Defer loading actions until control is loaded to ensure Handle created
        this.Load += ActionsView_Load;
    }

    private void ActionsView_Load(object? sender, EventArgs e)
    {
        try
        {
            LoadActions();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load actions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadActions()
    {
        _items.Clear();

        foreach ((Type type, ActionAttribute attribute) in FindActions(Assembly.GetExecutingAssembly()))
        {
            _items.Add(new ActionItem
            {
                Type = type,
                Id = attribute.Id,
                DisplayName = attribute.DisplayName,
                Description = attribute.Description
            });
        }

        lstActions.DataSource = null;
        lstActions.DisplayMember = nameof(ActionItem.DisplayName);
        lstActions.ValueMember = nameof(ActionItem.Id);
        lstActions.DataSource = _items;

        if (_items.Count > 0)
        {
            lstActions.SelectedIndex = 0;
        }
    }

    private void lstActions_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstActions.SelectedItem is not ActionItem item)
            return;

        try
        {
            if (!(Activator.CreateInstance(item.Type) is ActionControl ctrl))
                throw new InvalidOperationException($"Could not create action control: {item.DisplayName}");

            ctrl.Dock = DockStyle.Fill;
            ctrl.Initialize(Host);

            pnlHost.SuspendLayout();
            pnlHost.Controls.Clear();
            pnlHost.Controls.Add(ctrl);
            pnlHost.ResumeLayout();

            _current = ctrl;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to open action '{item.DisplayName}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
