using System.ComponentModel.DataAnnotations;
using Services.Models.Base;

namespace Services.Models.Station;

public class StationDto : TrackableModel
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Code { get; set; }
    [Required]
    [MaxLength(300)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string ContactPerson { get; set; }
    [Required]
    public string ContactEmail { get; set; }
    [Required]
    [MaxLength(50)]
    public string ContactPhone { get; set; }
    [MaxLength(100)]
    public string? ContactPersonAnother { get; set; }
    public string? ContactEmailAnother { get; set; }
    [MaxLength(50)]
    public string? ContactPhoneAnother { get; set; }
    [MaxLength(50)]
    public string? Address { get; set; }
    public decimal? Lat { get; set; }
    public decimal? Long { get; set; }
    public string Status { get; set; } = "Active";
}
