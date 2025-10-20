namespace StorageCleaner.Views;

public partial class SettingsView : PluginUserControlBase
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void btnExportManifest_Click(object sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog { Filter = "JSON|*.json", FileName = "manifest.json" };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            System.IO.File.WriteAllText(sfd.FileName, "{ \"version\": 1 }\n");
            MessageBox.Show("Manifest exported.");
        }
    }

    private void btnImportManifest_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog { Filter = "JSON|*.json" };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            var json = System.IO.File.ReadAllText(ofd.FileName);
            MessageBox.Show("Manifest imported (placeholder).\n" + json);
        }
    }
}
