
namespace Databases.Entities;

public class DataAttribute:AggregateRoot<Guid>
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? NameVI { get; set; }
    public string? NameEN { get; set; }
    public string DataType { get; set; }
    public string? DataValue { get; set; }
    public string? Metadata { get; set; }
}