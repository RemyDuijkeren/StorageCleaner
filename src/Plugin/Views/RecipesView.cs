using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;

namespace DataverseStorageCleaner.Views;

public partial class RecipesView : ViewBase
{
    public RecipesView()
    {
        InitializeComponent();
    }

    private void btnCreateOrUpdateJobs_Click(object sender, EventArgs e)
    {
        MessageBox.Show("This would create or update bulk delete jobs based on recipes.");
    }
}
