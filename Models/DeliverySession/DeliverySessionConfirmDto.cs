namespace Services.Models.DeliverySession;

public class DeliverySessionConfirmDto
{
    public string? Note { get; set; }
    public string? ReasonCancel { get; set; }
    public string? Excepted { get; set; }
    public int? TotalReceivedItems { get; set; }
    public string? DriverCode { get; set; }
}