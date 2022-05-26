using VkNet.Model;
using VkNet.Model.Attachments;
using VkViral.Dto.Posts;

namespace VkViral.Helpers;

public static class PostHelper
{
    public static PostDto Map(Post post, Group group, ulong groupPosts) =>
        new PostDto
        {
            GroupName = group.Name,
            GroupImg = group.Photo200,
            PublicationDate = post.Date,
            Text = post.Text,
            Images = post.Attachments
                .Where(x => x.Type == typeof(Photo))
                .Select(x => ((Photo) x.Instance).Sizes.LastOrDefault()!.Url)
                .ToList(),
            Videos = post.Attachments.Where(x => x.Type == typeof(Video))
                .Select(x => ((Video) x.Instance).Image.LastOrDefault()!.Url)
                .ToList(),
            Audios = post.Attachments
                .Where(x => x.Type == typeof(Audio)).Select(x => ((Audio) x.Instance).Url)
                .ToList(),
            Likes = post.Likes?.Count ?? 0,
            Comments = post.Comments?.Count ?? 0,
            Reposts = post.Reposts?.Count ?? 0,
            Views = post.Views?.Count ?? 0,
            GroupPostCount = (int)groupPosts,
            GroupMemberCount = group.MembersCount
        };
}