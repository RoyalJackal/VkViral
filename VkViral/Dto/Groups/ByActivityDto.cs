using VkNet.Model;
using VkViral.Dto.Auth;

namespace VkViral.Dto.Groups;

public class ByActivityDto
{
    public string Query { get; set; }
    public string Activity { get; set; }
    public AuthDto Auth { get; set; }
}