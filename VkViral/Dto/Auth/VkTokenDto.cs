using Newtonsoft.Json;

namespace VkViral.Dto.Auth;

public class VkTokenDto
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
  
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
    
    [JsonProperty("user_id")]
    public int UserId { get; set; }
}