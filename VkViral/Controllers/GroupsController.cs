using Microsoft.AspNetCore.Mvc;
using VkViral.Dto.Groups;
using VkViral.Services;

namespace VkViral.Controllers;

[Route("[controller]")]
[ApiController]
public class GroupsController : Controller
{
    private readonly GroupsService _groups;
    private readonly VkService _vk;

    public GroupsController(GroupsService groups, VkService vk)
    {
        _groups = groups;
        _vk = vk;
    }

    [HttpPost("CurrentUser")]
    public async Task<IActionResult> Get()
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();
        
        var result = await _groups.GetByCurrentUserAsync(vk);
        
        await vk.LogOutAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetById(string groupId)
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();

        var result = await _groups.GetByIdAsync(vk, groupId);
        
        await vk.LogOutAsync();
        return Ok(result);
    }
    
    [HttpPost("Ids")]
    public async Task<IActionResult> GetByIds([FromBody]ByIdsDto dto)
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();

        var result = await _groups.GetByIdsAsync(vk, dto.GroupIds);
        
        await vk.LogOutAsync();
        return Ok(result);
    }
    
    [HttpPost("Activity")]
    public async Task<IActionResult> GetByActivity([FromBody]ByActivityDto dto)
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();

        var result = await _groups.GetByActivityAsync(vk, dto.Activity, dto.Query);
        
        await vk.LogOutAsync();
        return Ok(result);
    }
    
    [HttpPost("Query")]
    public async Task<IActionResult> GetByQuery([FromBody]ByQueryDto dto)
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();

        var result = await _groups.GetByQueryAsync(vk, dto.Query);
        
        await vk.LogOutAsync();
        return Ok(result);
    }
}