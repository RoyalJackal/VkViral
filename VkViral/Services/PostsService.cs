using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using VkViral.Dto.Posts;
using VkViral.Helpers;

namespace VkViral.Services;

public class PostsService
{
    public async Task<List<PostDto>> GetPostsInGroupAsync(VkApi? vk, string groupId)
    {
        var group = vk.Groups.GetById(new List<string>(), groupId, GroupsFields.All).FirstOrDefault();
        if (group == null)
            return new List<PostDto>();
        var posts = await vk.Wall.GetAsync(new WallGetParams 
        {
            OwnerId = -group.Id,
            Offset = 0,
            Count = 100
        });
        var result = posts
            .WallPosts
            .Select(x => PostHelper.Map(x, group, posts.TotalCount))
            .ToList();

        return result;
    }
}