
namespace Services.Helper.Exceptions.Station;

public class StationDuplicatedCodeException : ApiException
{
    public StationDuplicatedCodeException() : base()
    {
        ErrorCode = "STATION_DUPLICATED_CODE";
        ErrorMessages = new List<string>()
        {
            "Mã trạm đã được sử dụng"
        };
    }
}