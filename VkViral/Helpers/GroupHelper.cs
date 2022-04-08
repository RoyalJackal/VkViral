using VkNet.Model;
using VkViral.Dto.Groups;

namespace VkViral.Helpers;

public static class GroupHelper
{
    public static GroupDto Map(Group group) =>
        new GroupDto
        {
            GroupId = group.Id,
            GroupName = group.Name,
            GroupImg = group.Photo200,
            GroupTheme = group.Activity
        };
}