using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility;

namespace DataverseStorageCleaner;

public partial class MainControl : PluginControlBase
{
    Settings mySettings;

    public MainControl()
    {
        InitializeComponent();
    }

    void MainControl_Load(object sender, EventArgs e)
    {
        ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

        // Loads or creates the settings for the plugin
        if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
        {
            mySettings = new Settings();

            LogWarning("Settings not found => a new settings file has been created!");
        }
        else
        {
            LogInfo("Settings found and loaded");
        }
    }

    void tsbClose_Click(object sender, EventArgs e) => CloseTool();

    void tsbSample_Click(object sender, EventArgs e)
    {
        // The ExecuteMethod method handles connecting to an organization if XrmToolBox is not yet connected
        ExecuteMethod(GetDatabaseSettings);
    }

    void GetAccounts() =>
        WorkAsync(new WorkAsyncInfo
        {
            Message = "Getting accounts",
            Work = (worker, args) =>
            {
                args.Result = Service.RetrieveMultiple(new QueryExpression("account")
                {
                    TopCount = 50
                });
            },
            PostWorkCallBack = (args) =>
            {
                if (args.Error != null)
                {
                    MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                var result = args.Result as EntityCollection;
                if (result != null)
                {
                    MessageBox.Show($"Found {result.Entities.Count} accounts");
                }
            }
        });

    void GetDatabaseSettings() =>
        WorkAsync(new WorkAsyncInfo
        {
            Message = "Getting Database Settings",
            Work = (worker, args) =>
            {
                var who = (WhoAmIResponse)Service.Execute(new WhoAmIRequest());
                var org = Service.Retrieve("organization", who.OrganizationId, new ColumnSet("orgdborgsettings"));
                args.Result = org.GetAttributeValue<string>("orgdborgsettings") ?? "<settings/>";
            },
            PostWorkCallBack = (args) =>
            {
                if (args.Error != null)
                {
                    MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var xml = args.Result as string;

                // Parse to dictionary (name -> value)
                var xdoc = System.Xml.Linq.XDocument.Parse(xml);
                var kv = xdoc.Root?
                             .Elements() // direct children of <OrgSettings>
                             .ToDictionary(
                                 e => e.Name.LocalName,
                                 e => e.Value
                             ) ?? new Dictionary<string, string>();

                // example
                //var quickFindLimit = kv.TryGetValue("QuickFindRecordLimitEnabled", out var v) ? v : "(not set)";


                // Display the results in a message box as a list of key/value pairs
                listBox1.Items.Clear();
                foreach (var kvp in kv)
                {
                    listBox1.Items.Add($"{kvp.Key}: {kvp.Value}");
                }
                MessageBox.Show(xml);


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

    /// <summary>This event occurs when the plugin is closed</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void MainControl_OnCloseTool(object sender, EventArgs e)
    {
        // Before leaving, save the settings
        SettingsManager.Instance.Save(GetType(), mySettings);
    }

    /// <summary>This event occurs when the connection has been updated in XrmToolBox</summary>
    public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
    {
        base.UpdateConnection(newService, detail, actionName, parameter);

        if (mySettings != null && detail != null)
        {
            mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
            LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
        }
    }
}
