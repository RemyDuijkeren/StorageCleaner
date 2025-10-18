using XrmToolBox.Extensibility;

namespace DataverseStorageCleaner.Views.Actions
{
    /// <summary>
    /// Descriptor for an action that can be plugged into ActionsView.
    /// Each action provides metadata and a way to create its UI control.
    /// </summary>
    public interface IAction
    {
        /// <summary>Unique id (stable) for this action.</summary>
        string Id { get; }
        /// <summary>Display name shown in the left navigation.</summary>
        string Name { get; }
        /// <summary>Optional description or tooltip.</summary>
        string? Description { get; }

        /// <summary>
        /// Create the control that renders the action UI. The control should derive from ActionControlBase.
        /// </summary>
        ActionControlBase CreateControl();
    }
}
