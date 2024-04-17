using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinPro.DTOs;
using TinPro.Models;
using TinPro.Services;
namespace TinPro.Controllers;
[ApiController]
[Route("api/login")]
public class LoginController : Controller
{

    private IDbService _dbService;

    public LoginController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost]
    public async Task<IActionResult> login(LoginDTO loginDto)
    {
        var user = await _dbService.Authenticate(loginDto);
        if (user is null) return NotFound();
        var token = await _dbService.GenerateToken(user);
        return Ok(new {token=token,role=user.Role.Name});

    }
}