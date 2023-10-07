using System.ComponentModel;

public enum EventTypeEnum
{
    [Description("Saved")]
    Saved = 1,

    [Description("Rented")]
    Rented = 2,

    [Description("Returned")]
    Returned = 3
}
