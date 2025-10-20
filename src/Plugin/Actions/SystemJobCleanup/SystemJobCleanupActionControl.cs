using XrmToolBox.Extensibility;

namespace StorageCleaner.Actions.SystemJobCleanup;

[Action("SystemJobCleanup", "System Job Cleanup", "Automatic deletion of System Jobs")]
public partial class SystemJobCleanupActionControl : ActionControl
{
    readonly SystemJobCleanupService _service = new();
    SystemJobCleanupFeature _feature = new();

    public SystemJobCleanupActionControl()
    {
        InitializeComponent();

        // Initialize UI with default property values
        chkEnableSystemJobCleanup.Checked = _feature.EnableSystemJobCleanup;

        numSucceededDays.Minimum = 0;
        numSucceededDays.Maximum = _feature.MaxPersistenceDaysForSucceededSystemJobs;
        numSucceededDays.Value = Math.Min(Math.Max(_feature.SucceededSystemJobPersistenceInDays, (int)numSucceededDays.Minimum), (int)numSucceededDays.Maximum);

        numCanceledDays.Minimum = 0;
        numCanceledDays.Maximum = _feature.MaxPersistenceDaysForCanceledOrFailedSystemJobs;
        numCanceledDays.Value = Math.Min(Math.Max(_feature.CanceledSystemJobPersistenceInDays, (int)numCanceledDays.Minimum), (int)numCanceledDays.Maximum);

        numFailedDays.Minimum = 0;
        numFailedDays.Maximum = _feature.MaxPersistenceDaysForCanceledOrFailedSystemJobs;
        numFailedDays.Value = Math.Min(Math.Max(_feature.FailedSystemJobPersistenceInDays, (int)numFailedDays.Minimum), (int)numFailedDays.Maximum);

        // Disable inputs until settings are loaded (Analyze)
        chkEnableSystemJobCleanup.Enabled = false;
        numSucceededDays.Enabled = false;
        numCanceledDays.Enabled = false;
        numFailedDays.Enabled = false;
    }

    public override void AnalyzeClick(object sender, EventArgs e) => ExecuteMethod(LoadSettings);

    public override void CleanClick(object sender, EventArgs e) => ExecuteMethod(SaveSettings);

    void LoadSettings() => WorkAsync(new WorkAsyncInfo
    {
        Message = "Loading system job cleanup settings...",
        Work = (worker, args) =>
        {
            var orgId = MainControl.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId;

            var settings = _service.Load(MainControl.Service, orgId);
            //Thread.Sleep(2000);

            SetWorkingMessage("Loading orgdborgsettings for grid...");
            var dict = _service.LoadDictionary(MainControl.Service, orgId);
            //Thread.Sleep(3000);

            args.Result = (settings, dict);
        },
        PostWorkCallBack = args =>
        {
            if (args.Error != null)
            {
                MainControl.ShowErrorDialog(args.Error);
                return;
            }

            var (settings, orgSettings) = ((SystemJobCleanupFeature settings, Dictionary<string, string> all) )args.Result;

            // Bind to controls
            chkEnableSystemJobCleanup.Checked = settings.EnableSystemJobCleanup;
            numSucceededDays.Value = Math.Min(Math.Max(settings.SucceededSystemJobPersistenceInDays, (int)numSucceededDays.Minimum), (int)numSucceededDays.Maximum);
            numCanceledDays.Value = Math.Min(Math.Max(settings.CanceledSystemJobPersistenceInDays, (int)numCanceledDays.Minimum), (int)numCanceledDays.Maximum);
            numFailedDays.Value = Math.Min(Math.Max(settings.FailedSystemJobPersistenceInDays, (int)numFailedDays.Minimum), (int)numFailedDays.Maximum);

            // Update backing field
            _feature = settings;

            // Populate the grid with settings
            gridOrgSettings.SuspendLayout();
            gridOrgSettings.Rows.Clear();
            foreach (var kv in orgSettings)
            {
                gridOrgSettings.Rows.Add(kv.Key, kv.Value);
            }
            gridOrgSettings.ResumeLayout();

            UpdateInputsEnabledState();
        }
    });

    void SaveSettings() => WorkAsync(new WorkAsyncInfo
    {
        Message = "Saving system job cleanup settings...",
        Work = (worker, args) =>
        {
            var orgId = MainControl.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId;
            args.Result = _service.Save(MainControl.Service, orgId, _feature);
        },
        PostWorkCallBack = args =>
        {
            if (args.Error != null)
            {
                MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var (ok, error) = ((bool ok, string? error))args.Result;
            if (!ok)
            {
                MessageBox.Show(error ?? "Unknown validation error", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Settings saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reload to reflect latest persisted values in grid
            AnalyzeClick(this, EventArgs.Empty);
        }
    });

    private void chkEnableSystemJobCleanup_CheckedChanged(object sender, EventArgs e)
    {
        _feature.EnableSystemJobCleanup = chkEnableSystemJobCleanup.Checked;
        UpdateInputsEnabledState();
    }

    private void numSucceededDays_ValueChanged(object sender, EventArgs e)
    {
        _feature.SucceededSystemJobPersistenceInDays = (int)numSucceededDays.Value;
    }

    private void numCanceledDays_ValueChanged(object sender, EventArgs e)
    {
        _feature.CanceledSystemJobPersistenceInDays = (int)numCanceledDays.Value;
    }

    private void numFailedDays_ValueChanged(object sender, EventArgs e)
    {
        _feature.FailedSystemJobPersistenceInDays = (int)numFailedDays.Value;
    }

    private void UpdateInputsEnabledState()
    {
        chkEnableSystemJobCleanup.Enabled = true;

        var enabled = chkEnableSystemJobCleanup.Checked;
        numSucceededDays.Enabled = enabled;
        numCanceledDays.Enabled = enabled;
        numFailedDays.Enabled = enabled;
    }
}

// Audit table:
// SELECT COUNT(*) AS [Rows],
// MIN(createdon) AS [Oldest],
// MAX(createdon) AS [Newest]
// FROM audit
//
// var request = new DeleteAuditDataRequest
// {
//     EndDate = DateTime.UtcNow.AddMonths(-6)
// };
// service.Execute(request);
