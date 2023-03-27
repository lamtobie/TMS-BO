namespace Services.Models.Delivering.Station;

public class DeliveringStationDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal? Lat { get; set; }
    public decimal? Long { get; set; }
    public string Status { get; set; }
}