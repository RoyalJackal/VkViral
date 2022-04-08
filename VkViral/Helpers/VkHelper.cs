using System.Security.Cryptography.X509Certificates;
using VkNet;
using VkNet.Model;

namespace VkViral.Helpers;

public static class VkHelper
{
    public static async Task<VkApi> GetClientAsync(string token)
    {
        var api = new VkApi();
        await api.AuthorizeAsync(new ApiAuthParams {AccessToken = token});
        return api;
    }
}