using Microsoft.AspNetCore.Mvc;
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
        var result = await _auth.Authorize(redirectUri, code, HttpContext.Response);
        if (result == false)
            return BadRequest();
        
        return Ok();
    }
    
    [HttpGet("User")]
    public async Task<IActionResult> CurrentUser()
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();
        
        var result = await _auth.GetCurrentUser(vk);
        if (result == null)
            return BadRequest();

        await vk.LogOutAsync();
        return Ok(result);
    }

    [HttpGet("Activities")]
    public async Task<IActionResult> Activities()
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();
        
        var result = await _auth.GetActivities(vk);

        await vk.LogOutAsync();
        return Ok(result);
    }
}