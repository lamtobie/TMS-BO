using System.ComponentModel;

namespace Services.Helper.Enums
{
    public enum ErrorCodes : uint
    {
        [Description("Error invalid context")]
        Err_InvalidContext = 0x0001,
        [Description("Error get shipment order from external")]
        Err_GetShipmentOrderSessionFromExternal = 0x0002
    }
}