using VkViral.Dto.Auth;
using VkViral.Enum;

namespace VkViral.Dto.Posts;

public class InGroupDto
{
    public string GroupId { get; set; }
    
    public SortType SortType { get; set; }
}