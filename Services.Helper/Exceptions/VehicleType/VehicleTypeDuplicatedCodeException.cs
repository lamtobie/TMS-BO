using Exceptions;

namespace Services.Helper.Exceptions.VehicleType;

public class VehicleTypeDuplicatedCodeException : ApiException
{
    public VehicleTypeDuplicatedCodeException() : base()
    {
        ErrorCode = "VEHICLETYPE_DUPLICATED_CODE";
        ErrorMessages = new List<string>()
        {
            "Loại phương tiện đã tồn tại"
        };
    }
}
