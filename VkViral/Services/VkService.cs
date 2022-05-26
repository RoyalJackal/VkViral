using Microsoft.EntityFrameworkCore;
using VkNet;
using VkNet.Model;
using VkViral.Data;
using VkViral.Helpers;

namespace VkViral.Services;

public class VkService
{
    private readonly EncryptionService? _encryptor;
    private readonly ApplicationDbContext _db;

    public VkService(EncryptionService? encryptor, ApplicationDbContext db)
    {
        _encryptor = encryptor;
        _db = db;
    }

    public async Task<VkApi?> GetClientAsync(HttpRequest request)
    {
        var cookie = CookieHelper.Get(request);
        if (cookie == null)
            return null;
        
        var token = await _db.Tokens.FirstOrDefaultAsync(x => x.Id == cookie.TokenId);
        if (token == null || token.UserId != cookie.UserId || token.ExpirationTime.Equals(cookie.ExpirationTime))
            return null;
        
        var decryptedToken = _encryptor.Decrypt(token.Value);
        
        var api = new VkApi();
        await api.AuthorizeAsync(new ApiAuthParams {AccessToken = decryptedToken});
        return api;
    }
}