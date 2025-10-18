namespace StorageCleaner.Views;

public partial class AnalyzeView : ViewBase
{
    public AnalyzeView()
    {
        InitializeComponent();
    }

    private void btnCreateAction_Click(object sender, EventArgs e)
    {
        // Raise an event others (MainControl) can handle to navigate
        CreateActionRequested?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? CreateActionRequested;
}
