using System.ComponentModel;

namespace Services.Helper.Enums;

public enum SessionStatusEnum
{
    [Description("Draft")]
    Draft,
    [Description("New")]
    New,
    [Description("HandedOver")]
    HandedOver,
    [Description("Cancelled")]
    Cancelled,
    [Description("Confirmed")]
    Confirmed,
    [Description("HandOverFailed")]
    HandOverFailed,
    [Description("PartialConfirm")]
    PartialConfirm,
    [Description("AConfirmed")]
    AConfirmed,
    [Description("BConfirmed")]
    BConfirmed
}