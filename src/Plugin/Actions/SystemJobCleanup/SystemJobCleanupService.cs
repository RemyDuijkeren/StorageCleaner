using System.Xml.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace StorageCleaner.Actions.SystemJobCleanup;

//https://learn.microsoft.com/en-us/power-platform/admin/cleanup-asyncoperationbase-table?tabs=new#set-retention-policy
// https://learn.microsoft.com/en-us/power-platform/admin/cleanup-asyncoperationbase-table
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

/// <summary>
/// Single service responsible for loading/saving orgdborgsettings for System Job Cleanup.
/// Performs validation, XML parsing and merging while preserving unrelated settings.
/// </summary>
public class SystemJobCleanupService
{
    // XML element names in orgdborgsettings
    const string RootName = "OrgSettings";
    const string EnableKey = "EnableSystemJobCleanup";
    const string SucceededKey = "SucceededSystemJobPersistenceInDays";
    const string CanceledKey = "CanceledSystemJobPersistenceInDays";
    const string FailedKey = "FailedSystemJobPersistenceInDays";

    public SystemJobCleanupFeature Load(IOrganizationService service, Guid organizationId)
    {
        if (service == null) throw new ArgumentNullException(nameof(service));

        var entity = service.Retrieve("organization", organizationId, new ColumnSet("orgdborgsettings"));
        var xml = entity.GetAttributeValue<string>("orgdborgsettings");

        var xdoc = ParseXmlOrCreate(xml);
        var root = xdoc.Root;

        var settings = new SystemJobCleanupFeature(); // defaults
        if (root != null)
        {
            var enableEl = root.Elements().FirstOrDefault(e => e.Name.LocalName == EnableKey);
            if (enableEl != null && bool.TryParse(enableEl.Value, out var enable))
                settings.EnableSystemJobCleanup = enable;

            var sucEl = root.Elements().FirstOrDefault(e => e.Name.LocalName == SucceededKey);
            if (sucEl != null && int.TryParse(sucEl.Value, out var suc))
                settings.SucceededSystemJobPersistenceInDays = suc;

            var canEl = root.Elements().FirstOrDefault(e => e.Name.LocalName == CanceledKey);
            if (canEl != null && int.TryParse(canEl.Value, out var can))
                settings.CanceledSystemJobPersistenceInDays = can;

            var failEl = root.Elements().FirstOrDefault(e => e.Name.LocalName == FailedKey);
            if (failEl != null && int.TryParse(failEl.Value, out var fail))
                settings.FailedSystemJobPersistenceInDays = fail;
        }

        // clamp softly using domain feature helpers
        settings.SucceededSystemJobPersistenceInDays = settings.ClampSucceeded(settings.SucceededSystemJobPersistenceInDays);
        settings.CanceledSystemJobPersistenceInDays = settings.ClampCanceled(settings.CanceledSystemJobPersistenceInDays);
        settings.FailedSystemJobPersistenceInDays = settings.ClampFailed(settings.FailedSystemJobPersistenceInDays);
        return settings;
    }

    /// <summary>
    /// Loads the current orgdborgsettings and returns the full settings dictionary.
    /// This is primarily for diagnostics/development and may be removed later.
    /// </summary>
    public Dictionary<string, string> LoadDictionary(IOrganizationService service, Guid organizationId)
    {
        if (service == null) throw new ArgumentNullException(nameof(service));
        var entity = service.Retrieve("organization", organizationId, new ColumnSet("orgdborgsettings"));
        var xml = entity.GetAttributeValue<string>("orgdborgsettings");
        var xdoc = ParseXmlOrCreate(xml);
        return xdoc.Root?.Elements().ToDictionary(e => e.Name.LocalName, e => e.Value)
               ?? new Dictionary<string, string>();
    }

    public (bool ok, string? error) Save(IOrganizationService service, Guid organizationId, SystemJobCleanupFeature feature)
    {
        if (service == null) throw new ArgumentNullException(nameof(service));
        if (feature == null) throw new ArgumentNullException(nameof(feature));

        var (ok, error) = feature.Validate();
        if (!ok)
            return (false, error);

        // Load existing to preserve unrelated settings
        var entity = service.Retrieve("organization", organizationId, new ColumnSet("orgdborgsettings"));
        var existingXml = entity.GetAttributeValue<string>("orgdborgsettings");
        var xdoc = ParseXmlOrCreate(existingXml);
        var root = xdoc.Root ?? new XElement(RootName);
        if (xdoc.Root == null) xdoc.Add(root);

        // Merge only four keys (create or update). Keep other elements intact.
        SetElement(root, EnableKey, feature.EnableSystemJobCleanup.ToString().ToLowerInvariant());
        SetElement(root, SucceededKey, feature.SucceededSystemJobPersistenceInDays.ToString());
        SetElement(root, CanceledKey, feature.CanceledSystemJobPersistenceInDays.ToString());
        SetElement(root, FailedKey, feature.FailedSystemJobPersistenceInDays.ToString());

        // Update organization entity
        var update = new Entity("organization")
        {
            Id = organizationId
        };
        update["orgdborgsettings"] = xdoc.ToString(SaveOptions.DisableFormatting);
        service.Update(update);

        return (true, null);
    }


    static XDocument ParseXmlOrCreate(string? xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
            return new XDocument(new XElement(RootName));
        try
        {
            return XDocument.Parse(xml);
        }
        catch
        {
            // Fallback to empty safe doc if unexpected content is stored
            return new XDocument(new XElement(RootName));
        }
    }

    static void SetElement(XElement root, string name, string value)
    {
        var el = root.Elements().FirstOrDefault(e => e.Name.LocalName == name);
        if (el == null)
        {
            root.Add(new XElement(name, value));
        }
        else
        {
            el.Value = value;
        }
    }
}
