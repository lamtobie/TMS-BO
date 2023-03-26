namespace Databases
{
    public interface ITrackable
    {
        long CreatedAt { get; set; }
        long? UpdatedAt { get; set; }
        long? CreatedBy { get; set; }
        long? UpdatedBy { get; set; }
    }
}
