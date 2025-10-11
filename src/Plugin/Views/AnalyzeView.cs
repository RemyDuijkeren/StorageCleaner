using Microsoft.Xrm.Sdk.Query;
using XrmToolBox.Extensibility;

namespace DataverseStorageCleaner.Views;

public partial class AnalyzeView : ViewBase
{
    public AnalyzeView()
    {
        InitializeComponent();
    }

    private void btnViewSample_Click(object sender, EventArgs e)
    {
        if (Host.Service == null)
        {
            MessageBox.Show("Not connected yet. Use Connect in XrmToolBox first.");
            return;
        }

        try
        {
            // Sample: retrieve first 5 systemjobs and show counts
            var qe = new QueryExpression("systemjob") { TopCount = 5, ColumnSet = new ColumnSet("name", "statecode", "statuscode") };
            var result = Host.Service.RetrieveMultiple(qe);
            MessageBox.Show($"Fetched {result.Entities.Count} sample system jobs.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error");
        }

        // The ExecuteMethod method handles connecting to an organization if XrmToolBox is not yet connected
        Host.ExecuteMethod(GetDatabaseSettings);
    }

    private void btnCreateRecipe_Click(object sender, EventArgs e)
    {
        // Raise an event others (MainControl) can handle to navigate
        CreateRecipeRequested?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? CreateRecipeRequested;

    void GetDatabaseSettings() => Host.WorkAsync(new WorkAsyncInfo
    {
        Message = "Getting Database Settings",
        Work = (worker, args) =>
        {
            // SetWorkingMessage("Getting Organization Settings");
            Thread.Sleep(3 * 1000);
            var org = Host.Service.Retrieve("organization", Host.ConnectionDetail.GetCrmServiceClient().ConnectedOrgId, new ColumnSet("orgdborgsettings"));
            args.Result = org.GetAttributeValue<string>("orgdborgsettings") ?? "<settings/>";
        },
        PostWorkCallBack = args =>
        {
            if (args.Error != null)
            {
                MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var xml = args.Result as string;

            // Parse to dictionary (name -> value)
            var xdoc = System.Xml.Linq.XDocument.Parse(xml);
            var orgSettings = xdoc.Root?
                         .Elements() // direct children of <OrgSettings>
                         .ToDictionary(
                             e => e.Name.LocalName,
                             e => e.Value
                         ) ?? new Dictionary<string, string>();

            // Populate the grid with settings
            gridOrgSettings.SuspendLayout();
            gridOrgSettings.Rows.Clear();
            foreach (var kv in orgSettings)
            {
                gridOrgSettings.Rows.Add(kv.Key, kv.Value);
            }
            gridOrgSettings.ResumeLayout();


            // some settings explantion:
            // https://support.microsoft.com/en-us/topic/orgdborgsettings-tool-for-microsoft-dynamics-crm-20a10f46-2a24-a156-7144-365d49b842ba

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
        }
    });
}
