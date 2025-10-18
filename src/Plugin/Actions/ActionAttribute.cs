namespace StorageCleaner.Actions;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ActionAttribute : Attribute
{
    public string Id { get; }
    public string DisplayName { get; }
    public string Description { get; }

    public ActionAttribute(string id, string displayName, string description)
    {
        Id = id;
        DisplayName = displayName;
        Description = description;
    }
}
