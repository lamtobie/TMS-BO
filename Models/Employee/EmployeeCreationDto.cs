using System.ComponentModel.DataAnnotations;

namespace Services.Models.Employee;

public class EmployeeCreationDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
    public string EmployeeType { get; set; }
    [Required]
    [MaxLength(50)]
    public string MobilePhone { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string StationCode { get; set; }
    public bool? IsStationAdmin { get; set; }
    [MaxLength(20)]
    public string? IdentityNumber { get; set; }
    public string? Email { get; set; }
    [MinLength(8)]
    [MaxLength(20)]
    public string? Password { get; set; }
    public string? Address { get; set; }
    public string? ThreePLTeam { get; set; }
    public string? AvatarPicture { get; set; }
    public string? DrivingLicensePicture { get; set; }
    public string? IdentityNumberPicture { get; set; }
    public string Status { get; set; } = "Active";
}