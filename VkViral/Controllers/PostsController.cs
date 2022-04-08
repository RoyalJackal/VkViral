using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VkViral.Data;
using VkViral.Dto.Groups;
using VkViral.Enum;
using VkViral.Helpers;
using VkViral.Services;

namespace VkViral.Controllers;

[Route("[controller]")]
[ApiController]
public class PostsController : Controller
{
    private readonly EncryptionService _encryptor;
    private readonly PostsService _posts;
    private readonly ApplicationDbContext _db;

    public PostsController(EncryptionService encryptor, PostsService posts, ApplicationDbContext db)
    {
        _encryptor = encryptor;
        _posts = posts;
        _db = db;
    }

    [HttpGet("InGroup")]
    public async Task<IActionResult> InGroup(string groupId, int tokenId, SortType sortType)
    {
        var token = await _db.Tokens.FirstOrDefaultAsync(x => x.Id == tokenId);
        if (token == null)
            return Unauthorized();     
        
        var decryptedToken = _encryptor.Decrypt(token.Value);
        var vk = await VkHelper.GetClientAsync(decryptedToken);

        var result = await _posts.GetPostsInGroupAsync(vk, groupId);

        await vk.LogOutAsync();
        return Ok(PostHelper.Sort(result, sortType));
    }
}