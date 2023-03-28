using Services.Models.Jwt;

namespace Services.Interfaces;

public interface IAuthorizedServices
{
    bool ValidatePermission();
    bool ValidatePermission(string permission);
    string? GetUserId();
}