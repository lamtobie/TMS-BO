using System.ComponentModel.DataAnnotations;
using Services.Models.Address;
using Services.Models.Base;

namespace Services.Models.Employee;

public class EmployeeDto : TrackableModel
{
    [Required]
    public string Code { get; set; }
    [Required]
    public string EmployeeType { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    public string MobilePhone { get; set; }
    [Required]
    public string StationCode { get; set; }
    public bool? IsStationAdmin { get; set; }
    public string? IdentityNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ThreePLTeam { get; set; }
    public string? AvatarPicture { get; set; }
    public string? DrivingLicensePicture { get; set; }
    public string? IdentityNumberPicture { get; set; }
    public string Status { get; set; } = "Active";
    public Guid? AddressId { get; set; }

    public AddressDto? Address { get; set; }
}