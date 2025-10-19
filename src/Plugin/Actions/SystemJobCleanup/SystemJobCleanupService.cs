using System.Xml.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace StorageCleaner.Actions.SystemJobCleanup;

/// <summary>
/// Single service responsible for loading/saving orgdborgsettings for System Job Cleanup.
/// Performs validation, XML parsing and merging while preserving unrelated settings.
/// </summary>
public class SystemJobCleanupService
{
    // XML element names in orgdborgsettings
    const string RootName = "OrgSettings"; // observed in sample; if not present, accept any root
    const string EnableKey = "EnableSystemJobCleanup";
    const string SucceededKey = "SucceededSystemJobPersistenceInDays";
    const string CanceledKey = "CanceledSystemJobPersistenceInDays";
    const string FailedKey = "FailedSystemJobPersistenceInDays";

    public async Task<SystemJobCleanupSettings> LoadAsync(IOrganizationService service, Guid organizationId)
    {
        var (settings, _) = await LoadAllAsync(service, organizationId);
        return settings;
    }

    /// <summary>
    /// Loads the current orgdborgsettings and returns both the typed settings and the full settings dictionary
    /// for display purposes.
    /// </summary>
    public async Task<(SystemJobCleanupSettings settings, Dictionary<string, string> allSettings)> LoadAllAsync(IOrganizationService service, Guid organizationId)
    {
        if (service == null) throw new ArgumentNullException(nameof(service));
        var entity = service.Retrieve("organization", organizationId, new ColumnSet("orgdborgsettings"));
        var xml = entity.GetAttributeValue<string>("orgdborgsettings");

        var xdoc = ParseXmlOrCreate(xml);
        var dict = xdoc.Root?.Elements().ToDictionary(e => e.Name.LocalName, e => e.Value)
                   ?? new Dictionary<string, string>();

        var settings = new SystemJobCleanupSettings(); // starts with defaults
        if (dict.TryGetValue(EnableKey, out var enableStr) && bool.TryParse(enableStr, out var enable))
            settings.Enable = enable;
        if (dict.TryGetValue(SucceededKey, out var sucStr) && int.TryParse(sucStr, out var suc))
            settings.SucceededDays = suc;
        if (dict.TryGetValue(CanceledKey, out var canStr) && int.TryParse(canStr, out var can))
            settings.CanceledDays = can;
        if (dict.TryGetValue(FailedKey, out var failStr) && int.TryParse(failStr, out var fail))
            settings.FailedDays = fail;

        // validate ranges softly (clamp to valid if data exists out of range)
        settings.SucceededDays = Clamp(settings.SucceededDays, 0, 90);
        settings.CanceledDays = Clamp(settings.CanceledDays, 0, 180);
        settings.FailedDays = Clamp(settings.FailedDays, 0, 180);

        return await Task.FromResult((settings, dict));
    }

    public async Task<(bool ok, string? error)> SaveAsync(IOrganizationService service, Guid organizationId, SystemJobCleanupSettings settings)
    {
        if (service == null) throw new ArgumentNullException(nameof(service));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        var validation = Validate(settings);
        if (validation != null)
            return (false, validation);

        // Load existing to preserve unrelated settings
        var entity = service.Retrieve("organization", organizationId, new ColumnSet("orgdborgsettings"));
        var existingXml = entity.GetAttributeValue<string>("orgdborgsettings");
        var xdoc = ParseXmlOrCreate(existingXml);
        var root = xdoc.Root ?? new XElement(RootName);
        if (xdoc.Root == null) xdoc.Add(root);

        // Merge only four keys (create or update). Keep other elements intact.
        SetElement(root, EnableKey, settings.Enable.ToString().ToLowerInvariant());
        SetElement(root, SucceededKey, settings.SucceededDays.ToString());
        SetElement(root, CanceledKey, settings.CanceledDays.ToString());
        SetElement(root, FailedKey, settings.FailedDays.ToString());

        // Update organization entity
        var update = new Entity("organization")
        {
            Id = organizationId
        };
        update["orgdborgsettings"] = xdoc.ToString(SaveOptions.DisableFormatting);
        service.Update(update);

        return await Task.FromResult<(bool ok, string? error)>((true, null));
    }

    static string? Validate(SystemJobCleanupSettings s)
    {
        if (s.SucceededDays is < 0 or > 90)
            return "SucceededDays must be between 0 and 90.";
        if (s.CanceledDays is < 0 or > 180)
            return "CanceledDays must be between 0 and 180.";
        if (s.FailedDays is < 0 or > 180)
            return "FailedDays must be between 0 and 180.";
        return null;
    }

    static int Clamp(int value, int min, int max) => value < min ? min : (value > max ? max : value);

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
