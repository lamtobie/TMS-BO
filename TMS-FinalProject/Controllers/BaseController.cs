using Services.Interfaces;
using Services.Models.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TMS_FinalProject.Controllers
{
   // [Authorize(Policy = Policy.Guest)]
    public class BaseController : ControllerBase
    {
        //private readonly IAuthorizedServices _authorizedServices;

        //public BaseController(IAuthorizedServices authorizedServices)
        //{
        //    _authorizedServices = authorizedServices;
        //}
    }
}