using Example.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(service.GetUsers());
    }
}
