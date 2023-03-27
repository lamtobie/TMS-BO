using System.ComponentModel;

namespace Services.Helper.Enums;

public enum SessionTypeEnum
{
    [Description("Pickup")]
    Pickup,
    [Description("Dropoff")]
    Dropoff,
    [Description("Refund")]
    Refund,
    [Description("Transit")]
    Transit
}