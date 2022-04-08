using Microsoft.AspNetCore.Mvc;
using VkViral.Services;

namespace VkViral.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth)
    {
        _auth = auth;
    }

    [HttpGet("Authorize")]
    public async Task<IActionResult> Authorize(Uri redirectUri, string code)
    {
        var result = await _auth.GetToken(redirectUri, code);
        if (result == null)
            return BadRequest();
        return Ok(result);
    }
}