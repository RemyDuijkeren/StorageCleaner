using Microsoft.Xrm.Sdk.Query;
using XrmToolBox.Extensibility;

namespace StorageCleaner.Actions.SystemJobCleanup;

[Action("SystemJobCleanup", "System Job Cleanup", "Automatic deletion of System Jobs")]
public partial class SystemJobCleanupActionControl : ActionControl
{
    // https://learn.microsoft.com/en-us/power-platform/admin/cleanup-asyncoperationbase-table?tabs=classic#set-retention-policy
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
        // The ExecuteMethod method handles connecting to an organization if XrmToolBox is not yet connected
        Host.ExecuteMethod(GetDatabaseSettings);
    }

    void GetDatabaseSettings() => Host.WorkAsync(new WorkAsyncInfo
    {
        Message = "Getting Database Settings",
        Work = (worker, args) =>
        {
            var org = Host.Service.Retrieve("organization", Host.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId, new ColumnSet("orgdborgsettings"));
            args.Result = org.GetAttributeValue<string>("orgdborgsettings");
        },
        PostWorkCallBack = args =>
        {
            if (args.Error != null) MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var xml = args.Result as string;
            var xDocument = System.Xml.Linq.XDocument.Parse(xml ?? "<settings/>");
            var orgSettings = xDocument.Root?
                                       .Elements()
                                       .ToDictionary(e => e.Name.LocalName, e => e.Value) ?? new Dictionary<string, string>();

            // Populate the grid with settings
            gridOrgSettings.SuspendLayout();
            gridOrgSettings.Rows.Clear();
            foreach (var kv in orgSettings)
            {
                gridOrgSettings.Rows.Add(kv.Key, kv.Value);
            }
            gridOrgSettings.ResumeLayout();
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
