using System.ComponentModel;

namespace Services.Helper.Enums;

public enum EmployeeTypeEnum
{
    [Description("Driver")]
    Driver,
    [Description("Coordinator")]
    Coordinator,
    [Description("StationManager")]
    StationManager,
    [Description("ClientOperationAdmin")]
    ClientOperationAdmin,
    [Description("ClientManagementAdmin")]
    ClientManagementAdmin
}