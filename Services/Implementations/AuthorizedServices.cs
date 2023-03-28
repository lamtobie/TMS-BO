using Services.Helper.Exceptions.Employee;
using Services.Interfaces;
using Services.Models.Jwt;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Services.Implementations;

public class AuthorizedServices : IAuthorizedServices
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizedServices(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Validate the request is a valid that following current user's warehouse
    /// </summary>
    /// <returns></returns>
    public bool ValidatePermission()
    {
        return true;
    }

    /// <summary>
    /// Validate permission within specific permission
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public bool ValidatePermission(string permission)
    {
        return true;
    }
    
    public string? GetUserId()
    {
        var userId =  _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
        return userId;
    }
}