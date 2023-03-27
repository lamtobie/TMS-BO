using System.ComponentModel.DataAnnotations;
using Services.Models.Address;
using Services.Models.Base;

namespace Services.Models.Station;

public class StationDto : TrackableModel
{
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string ContactPerson { get; set; }
    [Required]
    public string ContactEmail { get; set; }
    [Required]
    public string ContactPhone { get; set; }
    public string? ContactPersonAnother { get; set; }
    public string? ContactEmailAnother { get; set; }
    public string? ContactPhoneAnother { get; set; }

    public string Status { get; set; } = "Active";

    public Guid? AddressId { get; set; }

    public AddressDto? Address { get; set; }
}
