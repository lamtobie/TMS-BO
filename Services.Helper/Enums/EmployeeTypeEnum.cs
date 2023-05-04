using System.ComponentModel;

namespace Services.Helper.Enums;

public enum EmployeeTypeEnum
{
    [Description("driver")]
    Driver,
    [Description("coordinator")]
    Coordinator,
    [Description("stationmanager")]
    StationManager,
    [Description("ClientOperationAdmin")]
    ClientOperationAdmin,
    [Description("ClientManagementAdmin")]
    ClientManagementAdmin
}