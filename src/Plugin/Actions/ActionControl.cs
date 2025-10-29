namespace StorageCleaner.Actions;

/// <summary>
/// Base class for UI controls hosted inside ActionsView. Provides a uniform bottom action bar
/// with Analyze and Clean buttons, and a help section. Derived controls can override the
/// AnalyzeClick/CleanClick hooks and will have their existing child controls automatically
/// moved into the <see cref="ContentPanel"/>.
/// </summary>
public partial class ActionControl : PluginUserControl
{
    bool _adopted;

    public ActionControl()
    {
        InitializeComponent();
        HelpText = string.Empty;
    }

    /// <summary>Panel that hosts the derived control's custom UI.</summary>
    protected Panel ContentPanel => pnlContent;

    /// <summary>Gets or sets the help text. Supports URLs (clickable).</summary>
    public string HelpText
    {
        get => rtbHelp.Text;
        set => rtbHelp.Text = value ?? string.Empty;
    }

    public Button AnalyzeButton => btnAnalyze;
    public Button CleanButton => btnClean;

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        if (PluginUserControl.IsDesignMode())
        {
            // In designer, hide the base layout so derived controls are visible/editable
            layoutRoot.Visible = false;
            return;
        }
        ReparentChildControlsIntoContentPanel();
    }

    void ReparentChildControlsIntoContentPanel()
    {
        if (_adopted) return;
        _adopted = true;

        // Move any child controls (except our base ones) into the content panel
        // Important: keep the base layout root where it is to avoid circular parenting.
        var baseControls = new HashSet<Control> { layoutRoot, pnlContent, grpHelp, tlpButtons };
        var toMove = new List<Control>();
        foreach (Control c in Controls)
        {
            // Skip our base layout root and any of its descendants
            if (baseControls.Contains(c))
                continue;
            toMove.Add(c);
        }
        // Suspend layout to avoid flicker
        SuspendLayout();
        pnlContent.SuspendLayout();
        foreach (var c in toMove)
        {
            Controls.Remove(c);
            c.Dock = c.Dock; // keep existing docking
            pnlContent.Controls.Add(c);
        }
        pnlContent.ResumeLayout();
        ResumeLayout();
    }

    void btnAnalyze_Click(object? sender, EventArgs e) => AnalyzeClick(sender!, e);
    void btnClean_Click(object? sender, EventArgs e) => CleanClick(sender!, e);

    /// <summary>Optional hook so ActionsView can ask the control to load or analyze the current situation.</summary>
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
