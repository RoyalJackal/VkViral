using System.Text.Json;
using VkViral.Dto.Auth;
using VkViral.Model;

namespace VkViral.Helpers;

public static class CookieHelper
{
    private const string CookieName = "VkViral";
    
    public static void Set(HttpResponse response, Token token)
    {
        var cookie = new Cookie
        {
            TokenId = token.Id,
            ExpirationTime = token.ExpirationTime,
            UserId = token.UserId
        };
        var cookieJson = JsonSerializer.Serialize(cookie);

        response.Cookies.Append(CookieName, cookieJson, new CookieOptions
        {
            Expires = token.ExpirationTime,
            IsEssential = true
        });
    }

    public static Cookie? Get(HttpRequest request)
    {
        var cookie = request.Cookies[CookieName];
        if (cookie == null)
            return null;

        return JsonSerializer.Deserialize<Cookie>(cookie);
    }
}