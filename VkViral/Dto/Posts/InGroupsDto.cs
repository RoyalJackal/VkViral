using VkViral.Dto.Auth;
using VkViral.Enum;

namespace VkViral.Dto.Posts;

public class InGroupsDto
{
    public List<string> GroupIds { get; set; }
    
    public SortType SortType { get; set; }
}