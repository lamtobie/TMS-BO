using Services.Models.Jwt;

namespace Eton.TMS.OpBackOffice.Services.Interfaces;

public interface IAuthorizedServices
{
    bool ValidatePermission();
    bool ValidatePermission(string permission);
    string? GetUserId();
}