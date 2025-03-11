using Microsoft.AspNetCore.Mvc;
using StoreAPI.Application.DTOs;
using StoreAPI.Application.Interfaces;

namespace StoreAPI.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    // POST /api/auth/signup
    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SignupDto model)
    {
        await _userService.Signup(model);
        return CreatedAtAction(nameof(Signup), null);
    }

    // POST /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var token = await _userService.Login(model);

        HttpContext.Response.Cookies.Append("token", token, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(1),
            HttpOnly = true,
            IsEssential = true,
        });

        return Ok();
    }
}
