namespace StorageCleaner.Views;

public partial class JobsView : PluginUserControl
{
    public JobsView()
    {
        InitializeComponent();
    }

    public void RefreshJobs()
    {
        // Placeholder: in a real implementation, query for bulk delete jobs created by this tool
        gridJobs.Rows.Clear();
        gridJobs.Rows.Add("Cleanup Audit", "Ready", DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddDays(-1), 1234);
    }
}
