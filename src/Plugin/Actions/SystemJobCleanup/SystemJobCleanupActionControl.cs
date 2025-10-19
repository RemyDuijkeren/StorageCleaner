using XrmToolBox.Extensibility;

namespace StorageCleaner.Actions.SystemJobCleanup;

[Action("SystemJobCleanup", "System Job Cleanup", "Automatic deletion of System Jobs")]
public partial class SystemJobCleanupActionControl : ActionControl
{
    readonly SystemJobCleanupService _service = new();

    // Backing fields mirror the POCO for easy binding
    bool EnableSystemJobCleanup { get; set; } = true;
    int SucceededSystemJobPersistenceInDays { get; set; } = 30;
    int CanceledSystemJobPersistenceInDays { get; set; } = 60;
    int FailedSystemJobPersistenceInDays { get; set; } = 60;

    public SystemJobCleanupActionControl()
    {
        InitializeComponent();

        // Initialize UI with default property values
        chkEnableSystemJobCleanup.Checked = EnableSystemJobCleanup;

        numSucceededDays.Minimum = 0;
        numSucceededDays.Maximum = 90;
        numSucceededDays.Value = Math.Min(Math.Max(SucceededSystemJobPersistenceInDays, (int)numSucceededDays.Minimum), (int)numSucceededDays.Maximum);

        numCanceledDays.Minimum = 0;
        numCanceledDays.Maximum = 180;
        numCanceledDays.Value = Math.Min(Math.Max(CanceledSystemJobPersistenceInDays, (int)numCanceledDays.Minimum), (int)numCanceledDays.Maximum);

        numFailedDays.Minimum = 0;
        numFailedDays.Maximum = 180;
        numFailedDays.Value = Math.Min(Math.Max(FailedSystemJobPersistenceInDays, (int)numFailedDays.Minimum), (int)numFailedDays.Maximum);

        UpdateInputsEnabledState();
    }

    public override void AnalyzeClick(object sender, EventArgs e)
    {
        Host.ExecuteMethod(LoadSettings);
    }

    public override void CleanClick(object sender, EventArgs e)
    {
        Host.ExecuteMethod(SaveSettings);
    }

    void LoadSettings() => Host.WorkAsync(new WorkAsyncInfo
    {
        Message = "Loading system job cleanup settings...",
        Work = (worker, args) =>
        {
            var orgId = Host.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId;
            args.Result = _service.LoadAllAsync(Host.Service, orgId).GetAwaiter().GetResult();
        },
        PostWorkCallBack = args =>
        {
            if (args.Error != null)
            {
                MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var (settings, orgSettings) = ((SystemJobCleanupSettings settings, Dictionary<string, string> all) )args.Result;

            // Bind to controls
            chkEnableSystemJobCleanup.Checked = settings.Enable;
            numSucceededDays.Value = Math.Min(Math.Max(settings.SucceededDays, (int)numSucceededDays.Minimum), (int)numSucceededDays.Maximum);
            numCanceledDays.Value = Math.Min(Math.Max(settings.CanceledDays, (int)numCanceledDays.Minimum), (int)numCanceledDays.Maximum);
            numFailedDays.Value = Math.Min(Math.Max(settings.FailedDays, (int)numFailedDays.Minimum), (int)numFailedDays.Maximum);

            // Update backing fields
            EnableSystemJobCleanup = settings.Enable;
            SucceededSystemJobPersistenceInDays = settings.SucceededDays;
            CanceledSystemJobPersistenceInDays = settings.CanceledDays;
            FailedSystemJobPersistenceInDays = settings.FailedDays;

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

    void SaveSettings() => Host.WorkAsync(new WorkAsyncInfo
    {
        Message = "Saving system job cleanup settings...",
        Work = (worker, args) =>
        {
            var orgId = Host.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId;
            var poco = new SystemJobCleanupSettings
            {
                Enable = EnableSystemJobCleanup,
                SucceededDays = SucceededSystemJobPersistenceInDays,
                CanceledDays = CanceledSystemJobPersistenceInDays,
                FailedDays = FailedSystemJobPersistenceInDays
            };
            args.Result = _service.SaveAsync(Host.Service, orgId, poco).GetAwaiter().GetResult();
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
        EnableSystemJobCleanup = chkEnableSystemJobCleanup.Checked;
        UpdateInputsEnabledState();
    }

    private void numSucceededDays_ValueChanged(object sender, EventArgs e)
    {
        SucceededSystemJobPersistenceInDays = (int)numSucceededDays.Value;
    }

    private void numCanceledDays_ValueChanged(object sender, EventArgs e)
    {
        CanceledSystemJobPersistenceInDays = (int)numCanceledDays.Value;
    }

    private void numFailedDays_ValueChanged(object sender, EventArgs e)
    {
        FailedSystemJobPersistenceInDays = (int)numFailedDays.Value;
    }

    private void UpdateInputsEnabledState()
    {
        var enabled = chkEnableSystemJobCleanup.Checked;
        numSucceededDays.Enabled = enabled;
        numCanceledDays.Enabled = enabled;
        numFailedDays.Enabled = enabled;
    }
}

// <OrgSettings>
//     <IsRetentionEnabled>true</IsRetentionEnabled>
//     <IsArchivalEnabled>true</IsArchivalEnabled>
//     <IsPreferredSolutionEnabled>true</IsPreferredSolutionEnabled>
//     <TDSListenerInitialized>3</TDSListenerInitialized>
//     <CreateOnlyNonEmptyAddressRecordsForEligibleEntities>true</CreateOnlyNonEmptyAddressRecordsForEligibleEntities>
//     <IsMRUDataIngestionToSubstrateEnabled>true</IsMRUDataIngestionToSubstrateEnabled>
//     <SearchAndCopilotIndexMode>1</SearchAndCopilotIndexMode>
//     <EnableSystemJobCleanup>true</EnableSystemJobCleanup>
//     <CanceledSystemJobPersistenceInDays>10</CanceledSystemJobPersistenceInDays>
//     <FailedSystemJobPersistenceInDays>10</FailedSystemJobPersistenceInDays>
//     <SucceededSystemJobPersistenceInDays>5</SucceededSystemJobPersistenceInDays>
// </OrgSettings>

// some settings explanation:
// https://support.microsoft.com/en-us/topic/orgdborgsettings-tool-for-microsoft-dynamics-crm-20a10f46-2a24-a156-7144-365d49b842ba

// https://learn.microsoft.com/en-us/power-platform/admin/cleanup-asyncoperationbase-table?tabs=new
// EnableSystemJobCleanup Bool (default:True) This enables the Deletion Service to remove system jobs that have been completed or canceled.
// SucceededSystemJobPersistenceInDays Number (0-90) (default 30) This sets the amount of time that Succeeded system jobs are maintained before being removed by the Deletion Service.
// CanceledSystemJobPersistenceInDays Number (0-180) (default 60) This sets the amount of time that Canceled system jobs are maintained before being removed by the Deletion Service.
// FailedSystemJobPersistenceInDays Number (0-180) (default 60) This sets the amount of time that Failed system jobs are maintained before being removed by the Deletion Service.
// CreateOnlyNonEmptyAddressRecordsForEligibleEntities Bool (default:Fales?)
// traceLogPersistenceTimeInDays 30 (Int) This sets the amount of time that TraceLog data is maintained before being removed by the Deletion Service.

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
