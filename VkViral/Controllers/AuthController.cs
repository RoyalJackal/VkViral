using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VkNet;
using VkViral.Helpers;
using VkViral.Services;

namespace VkViral.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly AuthService _auth;
    private readonly VkService _vk;

    public AuthController(AuthService auth, VkService vk)
    {
        _auth = auth;
        _vk = vk;
    }

    [HttpGet("Authorize")]
    public async Task<IActionResult> Authorize(Uri redirectUri, string code)
    {
        var result = await _auth.GetToken(redirectUri, code);
        if (result == null)
            return BadRequest();
        
        return Ok(result);
    }
    
    [HttpGet("User")]
    public async Task<IActionResult> CurrentUser(int tokenId)
    {
        var vk = await _vk.GetClientAsync(tokenId);
        if (vk == null)
            return Unauthorized();
        
        var result = await _auth.GetCurrentUser(vk);
        if (result == null)
            return BadRequest();
        
        return Ok(result);
    }
}