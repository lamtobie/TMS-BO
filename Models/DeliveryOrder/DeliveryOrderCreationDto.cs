namespace Services.Models.DeliveryOrder;


public class Point
{
    public string? StationCode { get; set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
    public string? Note { get; set; }
}

public class PickupPoint : Point
{
    public int? ExpectedArrivalTime { get; set; }
}

public class Item
{
    public string? Code { get; set; }
    public string Name { get; set; }
    public string? ExternalCode { get; set; }
    public string? Uom { get; set; }
    public int? Quantity { get; set; }
    public int? Weight { get; set; }
    public int? Length { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
}

public class ReceivedPoint : Point
{
    public int? ExpectedStartTime { get; set; }
    public string? ReferenceCode { get; set; }
    public string ThreePLTeam { get; set; }
    public string ProductType { get; set; }
    public float Amount { get; set; }
    public int? Weight { get; set; }
    public List<Item> Items { get; set; }
    public bool? CodAlowed  { get; set; }
    public float? CodAmount  { get; set; }
    public string? CodMethod  { get; set; }
}

public class DeliveryOrderCreationDto
{
    public PickupPoint StartStation { get; set; }
    public List<ReceivedPoint> EndStations { get; set; }
}