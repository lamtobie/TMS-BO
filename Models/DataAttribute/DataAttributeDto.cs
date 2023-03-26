using Services.Models.Base;

namespace Services.Models.DataAttribute;

public class DataAttributeDto : TrackableModel
{
    public Guid? Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? NameVI { get; set; }
    public string? NameEN { get; set; }
    public string DataType { get; set; }
    public dynamic? DataValue { get; set; }
    public dynamic? Metadata { get; set; }
}