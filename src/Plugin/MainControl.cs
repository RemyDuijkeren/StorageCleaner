using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;

namespace DataverseStorageCleaner;

public partial class MainControl : PluginControlBase
{
    Settings _settings;

    public MainControl()
    {
        InitializeComponent();

        // Wire cross-view navigation/events
        analyzeView.CreateActionRequested += (s, e) => tabMain.SelectedTab = tabActions;
    }

    void MainControl_Load(object sender, EventArgs e)
    {
        //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

        // Loads or creates the settings for the plugin
        if (!SettingsManager.Instance.TryLoad(GetType(), out _settings))
        {
            _settings = new Settings();
            LogWarning("Settings not found => a new settings file has been created!");
        }
        else
        {
            LogInfo("Settings found and loaded");
        }

        // Initialize subviews with host and settings (Service/Connection are accessed through Host)
        analyzeView.Initialize(this, _settings);
        actionsView.Initialize(this, _settings);
        jobsView.Initialize(this, _settings);
        settingsView.Initialize(this, _settings);
    }

    void tsbClose_Click(object sender, EventArgs e) => CloseTool();

    void tsbSample_Click(object sender, EventArgs e)
    {
        // Navigate to Analyze tab as a quick entry point
        tabMain.SelectedTab = tabAnalyze;
        MessageBox.Show("Use 'View sample' on Analyze tab to fetch sample data.");
    }

    /// <summary>This event occurs when the plugin is closed</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void MainControl_OnCloseTool(object sender, EventArgs e)
    {
        // Before leaving, save the settings
        SettingsManager.Instance.Save(GetType(), _settings);
    }

    /// <summary>This event occurs when the connection has been updated in XrmToolBox</summary>
    public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
    {
        base.UpdateConnection(newService, detail, actionName, parameter);

        if (_settings != null && detail != null)
        {
            _settings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
            LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
        }

        // No need to update context on views; they read Service/Connection from Host dynamically
        jobsView.RefreshJobs();
    }
}
