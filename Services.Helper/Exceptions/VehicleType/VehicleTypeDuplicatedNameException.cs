using Exceptions;

namespace Services.Helper.Exceptions.VehicleType;

public class VehicleTypeDuplicatedNameException : ApiException
{
    public VehicleTypeDuplicatedNameException() : base()
    {
        ErrorCode = "VEHICLETYPE_DUPLICATED_NAME";
        ErrorMessages = new List<string>()
        {
            "Tên phương tiện đã tồn tại"
        };
    }
}
