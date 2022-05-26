using VkViral.Dto.Posts;
using VkViral.Enum;

namespace VkViral.Services;

public class SortingService
{
    private readonly PredictionService _prediction;

    public SortingService(PredictionService prediction)
    {
        _prediction = prediction;
    }
    
    public async Task<List<PostDto>> SortAsync(List<PostDto> posts, SortType sortType)
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
            case SortType.Predicted:
                return await SortByVirality(posts);
            default:
            {
                Console.WriteLine("Wrong sorting type.");
                return new List<PostDto>();
            }
        }
    }
    
    private List<PostDto> SortByLikes(List<PostDto> posts) =>
        posts.OrderByDescending(x => x.Likes).ToList();

    private List<PostDto> SortByViews(List<PostDto> posts) =>
        posts.OrderByDescending(x => x.Views).ToList();
    
    private List<PostDto> SortByWeighedLikes(List<PostDto> posts) =>
        posts.OrderByDescending(x => (double)x.Likes / x.GroupMemberCount).ToList();
    
    private List<PostDto> SortByWeighedViews(List<PostDto> posts) =>
        posts.OrderByDescending(x => (double)x.Views / x.GroupMemberCount).ToList();

    private List<PostDto> SortBySize(List<PostDto> posts) =>
        posts.OrderByDescending(x => x.Text.Length).ToList();

    private List<PostDto> SortByPublicationDate(List<PostDto> posts) =>
        posts.OrderByDescending(x => x.PublicationDate).ToList();

    private List<PostDto> SortByMedia(List<PostDto> posts) =>
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

    private async Task<List<PostDto>> SortByVirality(List<PostDto> posts) =>
        (await _prediction.PredictViralityAsync(posts))
        .OrderByDescending(x => x.Virality)
        .ToList();
}