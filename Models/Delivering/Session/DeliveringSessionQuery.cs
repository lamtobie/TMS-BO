using Services.Models.Base;

namespace Services.Models.Delivering.Session;

public class DeliveringSessionQuery : QueryableModel
{
    public string? EndStation { get; set; }
    public List<string>? SessionStatus { get; set; } = new List<string>();
    public bool IsTransit { get; set; } = false;
}