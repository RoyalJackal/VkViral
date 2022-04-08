using VkNet.Model;
using VkNet.Model.Attachments;
using VkViral.Dto.Posts;
using VkViral.Enum;

namespace VkViral.Helpers;

public static class PostHelper
{
    public static List<PostDto> Sort(List<PostDto> posts, SortType sortType)
    {
        switch (sortType)
        {
            case SortType.Likes:
                return SortByLikes(posts);
            case SortType.Views:
                return SortByViews(posts);
            case SortType.WeighedLikes:
                return SortByWeighedLikes(posts);
            case SortType.WeighedViews:
                return SortByWeighedViews(posts);
            case SortType.Size:
                return SortBySize(posts);
            case SortType.PublicationDate:
                return SortByPublicationDate(posts);
            case SortType.Media:
                return SortByMedia(posts);
            default:
            {
                Console.WriteLine("Wrong sorting type.");
                return new List<PostDto>();
            }
        }
    }

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
    
    private static List<PostDto> SortByLikes(List<PostDto> posts) =>
        posts.OrderByDescending(x => x.Likes).ToList();

    private static List<PostDto> SortByViews(List<PostDto> posts) =>
        posts.OrderByDescending(x => x.Views).ToList();
    
    private static List<PostDto> SortByWeighedLikes(List<PostDto> posts) =>
        posts.OrderByDescending(x => (double)x.Likes / x.GroupMemberCount).ToList();
    
    private static List<PostDto> SortByWeighedViews(List<PostDto> posts) =>
        posts.OrderByDescending(x => (double)x.Views / x.GroupMemberCount).ToList();

    private static List<PostDto> SortBySize(List<PostDto> posts) =>
        posts.OrderByDescending(x => x.Text.Length).ToList();

    private static List<PostDto> SortByPublicationDate(List<PostDto> posts) =>
        posts.OrderByDescending(x => x.PublicationDate).ToList();

    private static List<PostDto> SortByMedia(List<PostDto> posts) =>
        posts
            .Where(x => x.Videos.Any())
            .Concat(posts
                .Where(x => x.Images.Any()))
            .Concat(posts
                .Where(x => x.Audios.Any()))
            .Concat(posts
                .Where(x => x.Videos.Count == 0 && x.Images.Count == 0 && x.Audios.Count == 0))
            .Distinct()
            .ToList();
}