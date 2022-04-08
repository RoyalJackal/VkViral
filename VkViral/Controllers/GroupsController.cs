using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using VkViral.Data;
using VkViral.Dto.Groups;
using VkViral.Helpers;
using VkViral.Services;

namespace VkViral.Controllers;

[Route("[controller]")]
[ApiController]
public class GroupsController : Controller
{
    private readonly EncryptionService _encryptor;
    private readonly GroupsService _groups;
    private readonly ApplicationDbContext _db;

    public GroupsController(EncryptionService encryptor, GroupsService groups, ApplicationDbContext db)
    {
        _encryptor = encryptor;
        _groups = groups;
        _db = db;
    }

    [HttpGet("CurrentUser")]
    public async Task<IActionResult> Get(int tokenId)
    {
        var token = await _db.Tokens.FirstOrDefaultAsync(x => x.Id == tokenId);
        if (token == null)
            return Unauthorized();
        
        var decryptedToken = _encryptor.Decrypt(token.Value);
        var vk = await VkHelper.GetClientAsync(decryptedToken);
        
        var result = await _groups.GetByCurrentUserAsync(vk);
        
        await vk.LogOutAsync();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(string groupId, int tokenId)
    {
        var token = await _db.Tokens.FirstOrDefaultAsync(x => x.Id == tokenId);
        if (token == null)
            return Unauthorized();
        
        var decryptedToken = _encryptor.Decrypt(token.Value);
        var vk = await VkHelper.GetClientAsync(decryptedToken);

        var result = await _groups.GetByIdAsync(vk, groupId);
        
        await vk.LogOutAsync();
        return Ok(result);
    }
}