namespace Services.Models.Base;

public class TrackableModel
{
    public long? CreatedAt { get; set; }
    public long? UpdatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
}