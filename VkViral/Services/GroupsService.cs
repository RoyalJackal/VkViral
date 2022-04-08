using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkViral.Dto.Groups;
using VkViral.Helpers;

namespace VkViral.Services;

public class GroupsService
{
    public async Task<List<GroupDto>> GetByCurrentUserAsync(VkApi? vk, int? count = null)
    {
        var user = vk.Users.Get(new List<long>()).FirstOrDefault();
        if (user == null)
            return new List<GroupDto>();
        
        var result = (await vk.Groups
                .GetAsync(new GroupsGetParams
                {
                    UserId = user.Id,
                    Extended = true,
                    Fields = GroupsFields.Activity,
                    Count = count
                }))
            .Select(GroupHelper.Map)
            .ToList();

        return result;
    }
    
    public async Task<GroupDto?> GetByIdAsync(VkApi? vk, string groupId)
    {
        return (await vk.Groups
                .GetByIdAsync(new List<string>(), groupId, GroupsFields.Activity))
            .Select(GroupHelper.Map)
            .FirstOrDefault();
    }
    
    public async Task<List<GroupDto>?> GetByIdsAsync(VkApi? vk, List<string> groupIds)
    {
        return (await vk.Groups
                .GetByIdAsync(groupIds, null, GroupsFields.Activity))
            .Select(GroupHelper.Map)
            .ToList();
    }
    
    public async Task<List<GroupDto>?> GetByActivityAsync(VkApi? vk, string activity, string query)
    {
        var groups = (await vk.Groups
                .SearchAsync(new GroupsSearchParams
                {
                    Count = 1000,
                    Query = query
                }))
            .ToList();
        return (await vk.Groups
                .GetByIdAsync(groups.Select(x => x.Id.ToString()), null, GroupsFields.Activity))
            .Where(x => x.Activity == activity)
            .Select(GroupHelper.Map)
            .ToList();
    }
    
    public async Task<List<GroupDto>?> GetByQueryAsync(VkApi? vk, string query)
    {
        var groups = (await vk.Groups
                .SearchAsync(new GroupsSearchParams
                {
                    Count = 1000,
                    Query = query
                }))
            .ToList();
        return (await vk.Groups
                .GetByIdAsync(groups.Select(x => x.Id.ToString()), null, GroupsFields.Activity))
            .Select(GroupHelper.Map)
            .ToList();
    }
}