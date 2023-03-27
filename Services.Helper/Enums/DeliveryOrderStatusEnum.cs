using System.ComponentModel;

namespace Services.Helper.Enums;

public enum DeliveryOrderStatusEnum
{
    [Description("Draft")]
    Draft,
    [Description("New")]
    New,
    [Description("WaitToPick")]
    WaitToPick,
    [Description("Assigned")]
    Assigned,
    [Description("Picking")]
    Picking,
    [Description("Picked")]
    Picked,
    [Description("InTrasit")]
    InTransit,
    [Description("Dropped")]
    Dropped,
    // Coordinator confirm handing over from driver
    [Description("Confirmed")]
    Confirmed,
    [Description("DeliveringToStation")]
    DeliveringToStation,
    [Description("DeliveredToStationSuccess")]
    DeliveredToStationSuccess,
    [Description("DeliveredToStationFailure")]
    DeliveredToStationFailure,
    [Description("DeliveringToClient")]
    DeliveringToClient,
    [Description("DeliveredToClientSuccessful")]
    DeliveredToClientSuccessful,
    [Description("DeliveredToClientFailure")]
    DeliveredToClientFailure,
    [Description("DeliveredFailureAndRefunding")]
    DeliveredFailureAndReturning,
    [Description("DeliveredFailureAndRefunded")]
    DeliveredFailureAndReturned,
    [Description("DeliveryDelay")]
    DeliveryDelay,
    [Description("DeliveryDelayAndReturning")]
    DeliveryDelayAndReturning,
    [Description("DeliveryDelayAndReturned")]
    DeliveryDelayAndReturned,
    [Description("Cancel")]
    Cancel
}