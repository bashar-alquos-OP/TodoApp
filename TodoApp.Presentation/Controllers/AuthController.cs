using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTO.UserDTO;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.WebAPI.Controllers;


[ApiController]
[Route("/")]
public class AuthController : ControllerBase
{
    
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwt;

    public AuthController(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwt = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRequest request)
    {
        var user = new User
        {
            UserName = request.UserName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Ok(new { Token = _jwt.GenerateJwtToken(user) });
        }

        return Unauthorized();
    }
}
