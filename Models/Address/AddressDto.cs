using Services.Models.Base;

namespace Services.Models.Address;

public class AddressDto : TrackableModel
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
}
