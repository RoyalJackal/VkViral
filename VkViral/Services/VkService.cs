using Microsoft.EntityFrameworkCore;
using VkNet;
using VkNet.Model;
using VkViral.Data;

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

    public async Task<VkApi?> GetClientAsync(int tokenId)
    {
        var token = await _db.Tokens.FirstOrDefaultAsync(x => x.Id == tokenId);
        if (token == null)
            return null;
        
        var decryptedToken = _encryptor.Decrypt(token.Value);
        
        var api = new VkApi();
        await api.AuthorizeAsync(new ApiAuthParams {AccessToken = decryptedToken});
        return api;
    }
}