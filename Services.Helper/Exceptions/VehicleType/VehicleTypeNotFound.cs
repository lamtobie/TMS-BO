using Exceptions;

namespace Services.Helper.Exceptions.VehicleType;

public class VehicleTypeNotFound : ApiException
{
    public VehicleTypeNotFound() : base()
    {
        ErrorCode = "VEHICLETYPE_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Loại phương tiện không tồn tại"
        };
    }
}
