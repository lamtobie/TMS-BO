using Exceptions;

namespace Services.Helper.Exceptions.Vehicle;

public class VehicleNotFoundException : NotFoundException
{
    public VehicleNotFoundException() : base()
    {
        ErrorCode = "VEHICLE_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Mã phương tiện không được tìm thấy"
        };
    }
}