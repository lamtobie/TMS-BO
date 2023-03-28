using Exceptions;

namespace Services.Helper.Exceptions.Vehicle;

public class VehicleDuplicatedCodeException : ApiException
{
    public VehicleDuplicatedCodeException() : base()
    {
        ErrorCode = "VEHICLE_DUPLICATED_CODE";
        ErrorMessages = new List<string>()
        {
            "Mã phương tiện đã tồn tại"
        };
    }
}
