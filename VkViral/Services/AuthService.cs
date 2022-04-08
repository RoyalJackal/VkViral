using Newtonsoft.Json;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkViral.Data;
using VkViral.Dto.Auth;
using VkViral.Helpers;
using VkViral.Model;

namespace VkViral.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly EncryptionService _encryptor;
    private readonly ApplicationDbContext _db;
    private const string TokenPath = "https://oauth.vk.com/access_token";

    public AuthService(IConfiguration configuration, EncryptionService encryptor, ApplicationDbContext db)
    {
        _configuration = configuration;
        _encryptor = encryptor;
        _db = db;
    }
    public async Task<bool> Authorize(Uri redirectUri, string code, HttpResponse response)
    {
        var httpClient = HttpClientHelper.GetClient();
        var tokenResponse = await httpClient.SendAsync(new HttpRequestMessage(
            HttpMethod.Get,
            $@"{TokenPath}?" + 
            $@"client_id={_configuration["Application:Id"]}&" +
            $@"client_secret={_configuration["Application:Secret"]}&" +
            $@"redirect_uri={redirectUri}&" + 
            $@"code={code}"));
        
        var contentStream = await tokenResponse.Content.ReadAsStreamAsync();
        
        using var streamReader = new StreamReader(contentStream);
        using var jsonReader = new JsonTextReader(streamReader);
        
        var serializer = new JsonSerializer();
        
        try
        {
            var dto = serializer.Deserialize<VkTokenDto>(jsonReader);
            await _db.Tokens.AddAsync(new Token
            {
                Value = _encryptor.Encrypt(dto.AccessToken),
                ExpirationTime = (DateTimeOffset.Now + TimeSpan.FromSeconds(dto.ExpiresIn)).ToUniversalTime(),
                UserId = dto.UserId
            });
            await _db.SaveChangesAsync();

            var dbToken = _db.Tokens.FirstOrDefault(x => x.UserId == dto.UserId);
            CookieHelper.Set(response, dbToken);

            return true;
        }
        catch(JsonReaderException)
        {
            Console.WriteLine("Invalid JSON.");
            return false;
        } 
    }

    public async Task<UserDto?> GetCurrentUser(VkApi vk) =>
        (await vk.Users
            .GetAsync(new List<long>(), ProfileFields.Photo200))
            .Select(UserHelper.Map)
            .FirstOrDefault();
}