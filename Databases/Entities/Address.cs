
namespace Databases.Entities;

public class Address : AggregateRoot<Guid>
{
    public Guid Id { get; set; }
    public string? BlockAddress { get; set; }
    public string? ClusterAddress { get; set; }
    public string? QuarterAddress { get; set; }
    public string? SubQuarterAddress { get; set; }
    public string Text { get; set; }
    public string SlicCode { get; set; }
    public string SlicLabel { get; set; }
    public decimal Lat { get; set; }
    public decimal Long { get; set; }
    public string SlicRegion { get; set; }
    public string SlicLevel { get; set; }
    public string SlicWard { get; set; }
    public string SlicDistrict { get; set; }
    public string SlicProvince { get; set; }
    public ICollection<Station> Stations { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<DeliveryOrder> DeliveryOrderStartAddress { get; set; }
    public ICollection<DeliveryOrder> DeliveryOrderEndAddress { get; set; }

}
