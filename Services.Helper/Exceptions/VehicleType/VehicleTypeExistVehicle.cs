using Exceptions;

namespace Services.Helper.Exceptions.VehicleType;

public class VehicleTypeExistVehicle : ApiException
{
    public VehicleTypeExistVehicle() : base()
    {
        ErrorCode = "VEHICLETYPE_EXIST_VEHICLE";
        ErrorMessages = new List<string>()
        {
            "Loại phương tiện không thể xoá vì đã có phương tiện đang sử dụng."
        };
    }
}
