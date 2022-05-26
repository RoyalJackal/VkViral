using Microsoft.AspNetCore.Mvc;
using VkViral.Dto.Posts;
using VkViral.Services;

namespace VkViral.Controllers;

[Route("[controller]")]
[ApiController]
public class PostsController : Controller
{
    private readonly PostsService _posts;
    private readonly VkService _vk;
    private readonly GroupsService _groups;
    private readonly SortingService _sorting;

    public PostsController(PostsService posts, VkService vk, GroupsService groups, SortingService sorting)
    {
        _posts = posts;
        _vk = vk;
        _groups = groups;
        _sorting = sorting;
    }

    [HttpPost("InGroup")]
    public async Task<IActionResult> InGroup([FromBody]InGroupDto dto)
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();

        var result = await _posts.GetPostsInGroupAsync(vk, dto.GroupId);

        await vk.LogOutAsync();
        return Ok(await _sorting.SortAsync(result, dto.SortType));
    }
    
    [HttpPost("InGroups")]
    public async Task<IActionResult> InGroups([FromBody]InGroupsDto dto)
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();

        var result = new List<PostDto>();
        foreach (var groupId in dto.GroupIds)
            result.AddRange(await _posts.GetPostsInGroupAsync(vk, groupId));

        await vk.LogOutAsync();
        return Ok(await _sorting.SortAsync(result, dto.SortType));
    }
    
    [HttpPost("InCurrentUser")]
    public async Task<IActionResult> InCurrentUser([FromBody]InCurrentUserDto dto)
    {
        var vk = await _vk.GetClientAsync(HttpContext.Request);
        if (vk == null)
            return Unauthorized();

        var groups = await _groups.GetByCurrentUserAsync(vk, 10);
        var result = new List<PostDto>();
        foreach (var group in groups)
            result.AddRange(await _posts.GetPostsInGroupAsync(vk, group.GroupId.ToString()));

        await vk.LogOutAsync();
        return Ok(await _sorting.SortAsync(result, dto.SortType));
    }
}