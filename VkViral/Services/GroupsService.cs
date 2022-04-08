using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using VkViral.Dto.Groups;
using VkViral.Helpers;

namespace VkViral.Services;

public class GroupsService
{
    public async Task<List<GroupDto>> GetByCurrentUserAsync(VkApi vk)
    {
        var user = vk.Users.Get(new List<long>()).FirstOrDefault();
        if (user == null)
            return new List<GroupDto>();
        
        var result = (await vk.Groups
                .GetAsync(new GroupsGetParams
                {
                    UserId = user.Id,
                    Extended = true,
                    Fields = GroupsFields.Activity
                }))
            .Select(GroupHelper.Map)
            .ToList();

        return result;
    }
    
    public async Task<GroupDto?> GetByIdAsync(VkApi vk, string groupId)
    {
        return (await vk.Groups
                .GetByIdAsync(new List<string>(), groupId, GroupsFields.Activity))
            .Select(GroupHelper.Map)
            .FirstOrDefault();
    }
}