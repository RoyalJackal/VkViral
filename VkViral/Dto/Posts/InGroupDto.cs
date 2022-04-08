using VkViral.Dto.Auth;
using VkViral.Enum;

namespace VkViral.Dto.Posts;

public class InGroupDto
{
    public AuthDto Auth { get; set; }
    
    public string GroupId { get; set; }
    
    public SortType SortType { get; set; }
}