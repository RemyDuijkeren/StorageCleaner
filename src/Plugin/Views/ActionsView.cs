using System.Reflection;
using StorageCleaner.Actions;

namespace StorageCleaner.Views;

public partial class ActionsView : ViewBase
{
    private readonly List<IAction> _actions = new();
    private ActionControlBase? _current;

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
        _actions.Clear();
        var asm = Assembly.GetExecutingAssembly();
        var actionTypes = asm
            .GetTypes()
            .Where(t => typeof(IAction).IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null)
            .OrderBy(t => t.Name)
            .ToList();

        foreach (var t in actionTypes)
        {
            try
            {
                if (Activator.CreateInstance(t) is IAction action)
                {
                    _actions.Add(action);
                }
            }
            catch
            {
                // ignore faulty action types
            }
        }

        lstActions.DataSource = null;
        lstActions.DataSource = _actions;
        lstActions.DisplayMember = nameof(IAction.DisplayName);

        if (_actions.Count > 0)
        {
            lstActions.SelectedIndex = 0;
        }
    }

    private void lstActions_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstActions.SelectedItem is not IAction action)
            return;

        try
        {
            var ctrl = action.Instance();
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
            MessageBox.Show($"Failed to open action '{action.DisplayName}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
