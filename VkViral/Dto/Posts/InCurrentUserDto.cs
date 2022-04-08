using VkViral.Dto.Auth;
using VkViral.Enum;

namespace VkViral.Dto.Posts;

public class InCurrentUserDto
{
    public AuthDto Auth { get; set; }
    
    public SortType SortType { get; set; }
}