using System.ComponentModel;

public enum EventTypeEnum
{
    /// <summary>
    /// Event saved
    /// </summary>
    [Description("Saved")]
    Saved = 1,

    /// <summary>
    /// Event rented
    /// </summary>
    [Description("Rented")]
    Rented = 2,

    /// <summary>
    /// Event returned
    /// </summary>
    [Description("Returned")]
    Returned = 3
}
