using VkViral.Enum;

namespace VkViral.Dto.Groups;

public class RequestDto
{
    public string Token { get; set; }
    
    public SortType SortType { get; set; }
}