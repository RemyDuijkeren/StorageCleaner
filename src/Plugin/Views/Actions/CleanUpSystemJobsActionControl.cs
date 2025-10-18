using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk.Query;
using XrmToolBox.Extensibility;

namespace DataverseStorageCleaner.Views.Actions;

public partial class CleanUpSystemJobsActionControl : ActionControlBase
{
    private PluginControlBase? HostPcb => Host as PluginControlBase;

    public CleanUpSystemJobsActionControl()
    {
        InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        SaveChanges();
    }

    private void btnViewSample_Click(object sender, EventArgs e)
    {
        if (HostPcb?.Service == null)
        {
            MessageBox.Show("Not connected yet. Use Connect in XrmToolBox first.");
            return;
        }

        try
        {
            // Sample: retrieve first 5 systemjobs and show counts
            var qe = new QueryExpression("systemjob") { TopCount = 5, ColumnSet = new ColumnSet("name", "statecode", "statuscode") };
            var result = HostPcb.Service.RetrieveMultiple(qe);
            MessageBox.Show($"Fetched {result.Entities.Count} sample system jobs.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error");
        }

        // The ExecuteMethod method handles connecting to an organization if XrmToolBox is not yet connected
        HostPcb?.ExecuteMethod(GetDatabaseSettings);
    }

    public override void Initialize(object host, Settings settings)
    {
        base.Initialize(host, settings);
        if (Settings != null)
        {
            txtUrl.Text = Settings.LastUsedOrganizationWebappUrl;
        }
    }

    public override void SaveChanges()
    {
        if (Settings != null)
        {
            Settings.LastUsedOrganizationWebappUrl = txtUrl.Text?.Trim();
        }
    }

    private void GetDatabaseSettings() => HostPcb?.WorkAsync(new WorkAsyncInfo
    {
        Message = "Getting Database Settings",
        Work = (worker, args) =>
        {
            var org = HostPcb!.Service.Retrieve("organization", HostPcb.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId, new ColumnSet("orgdborgsettings"));
            args.Result = org.GetAttributeValue<string>("orgdborgsettings");
        },
        PostWorkCallBack = args =>
        {
            if (args.Error != null)
            {
                MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
