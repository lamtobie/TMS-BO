using Exceptions;

namespace Services.Helper.Exceptions.Station;

public class StationNotFoundException : ApiException
{
    public StationNotFoundException() : base()
    {
        ErrorCode = "STATION_NOT_FOUND";
        ErrorMessages = new List<string>()
        {
            "Trạm không tồn tại"
        };
    }
}