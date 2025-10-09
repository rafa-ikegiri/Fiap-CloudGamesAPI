using Core.Input;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudGamesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly IUsuarioRepository _userRepository;

    public AuthController(AuthService authService, IUsuarioRepository userRepository, IConfiguration configuration)
    {
        _authService = authService;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginInput logininput)
    {
        try
        {
            var result = await _authService.LoginAsync(logininput);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
