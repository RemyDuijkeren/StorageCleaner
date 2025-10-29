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

        // Provide contextual help for this action (shown in the base ActionControl help section)
        HelpText = "Configure automatic cleanup of System Jobs (asyncoperation).\n\n" +
                   "Use 'Analyze' to fetch current settings from the connected environment and view related orgdborgsettings.\n" +
                   "Use 'Clean' to validate and save your preferences.\n\n" +
                   "Tips:\n- Keep succeeded jobs short-lived to reduce storage.\n- Canceled/Failed jobs can be retained a bit longer for troubleshooting.\n\n" +
                   "Docs:\n- Microsoft: Manage system jobs (async operations): https://learn.microsoft.com/dynamics365/customerengagement/on-premises/developer/manage-async-jobs\n- Organization (orgdborg) Settings: https://learn.microsoft.com/power-platform/admin/organization-settings";

        // Initialize UI with default property values
        chkEnableSystemJobCleanup.Checked = _feature.EnableSystemJobCleanup;

        numSucceededDays.Minimum = 0;
        numSucceededDays.Maximum = _feature.MaxPersistenceDaysForSucceededSystemJobs;
        numSucceededDays.Value = _feature.SucceededSystemJobPersistenceInDays;

        numCanceledDays.Minimum = 0;
        numCanceledDays.Maximum = _feature.MaxPersistenceDaysForCanceledOrFailedSystemJobs;
        numCanceledDays.Value = _feature.CanceledSystemJobPersistenceInDays;

        numFailedDays.Minimum = 0;
        numFailedDays.Maximum = _feature.MaxPersistenceDaysForCanceledOrFailedSystemJobs;
        numFailedDays.Value = _feature.FailedSystemJobPersistenceInDays;

        // Init Clean button text to Apply and set as disabled
        CleanButton.Text = "Apply";
        _feature.MarkLoaded();
        SetApplyButtonEnabledState();

        // Disable inputs until settings are loaded (Analyze button is clicked)
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
            var orgId = PluginContext.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId;
            var settings = _service.Load(PluginContext.Service, orgId);
            args.Result = settings;
        },
        PostWorkCallBack = args =>
        {
            if (args.Error != null)
            {
                PluginContext.ShowErrorDialog(args.Error);
                return;
            }

            var settings = (SystemJobCleanupFeature)args.Result;

            // Update the backing field first
            _feature = settings;

            // Bind to controls without triggering a dirty state
            chkEnableSystemJobCleanup.Checked = settings.EnableSystemJobCleanup;
            numSucceededDays.Value = settings.SucceededSystemJobPersistenceInDays;
            numCanceledDays.Value = settings.CanceledSystemJobPersistenceInDays;
            numFailedDays.Value = settings.FailedSystemJobPersistenceInDays;

            UpdateControlEnableStates();
            SetApplyButtonEnabledState();
        }
    });

    void SaveSettings() => WorkAsync(new WorkAsyncInfo
    {
        Message = "Saving system job cleanup settings...",
        Work = (worker, args) =>
        {
            var orgId = PluginContext.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId;
            args.Result = _service.Save(PluginContext.Service, orgId, _feature);
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
            MessageBox.Show("Settings applied.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reload to reflect latest persisted values in grid
            AnalyzeClick(this, EventArgs.Empty);
        }
    });

    void chkEnableSystemJobCleanup_CheckedChanged(object sender, EventArgs e)
    {
        _feature.EnableSystemJobCleanup = chkEnableSystemJobCleanup.Checked;
        UpdateControlEnableStates();
        SetApplyButtonEnabledState();
    }

    void numSucceededDays_ValueChanged(object sender, EventArgs e)
    {
        _feature.SucceededSystemJobPersistenceInDays = (int)numSucceededDays.Value;
        SetApplyButtonEnabledState();
    }

    void numCanceledDays_ValueChanged(object sender, EventArgs e)
    {
        _feature.CanceledSystemJobPersistenceInDays = (int)numCanceledDays.Value;
        SetApplyButtonEnabledState();
    }

    void numFailedDays_ValueChanged(object sender, EventArgs e)
    {
        _feature.FailedSystemJobPersistenceInDays = (int)numFailedDays.Value;
        SetApplyButtonEnabledState();
    }

    void UpdateControlEnableStates()
    {
        chkEnableSystemJobCleanup.Enabled = true;

        var enabled = chkEnableSystemJobCleanup.Checked;
        numSucceededDays.Enabled = enabled;
        numCanceledDays.Enabled = enabled;
        numFailedDays.Enabled = enabled;
    }

    void SetApplyButtonEnabledState() => CleanButton.Enabled = _feature.IsDirty;
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
