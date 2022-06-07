using Newtonsoft.Json;
using VkViral.Dto.Posts;
using VkViral.Helpers;

namespace VkViral.Services;

public class PredictionService
{
    private readonly IConfiguration _configuration;
    private const string PredictPath = "/predict";

    public PredictionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<PostDto>> PredictViralityAsync(List<PostDto> posts)
    {
        var httpClient = HttpClientHelper.GetClient();
        var response = await httpClient.PostAsJsonAsync(
            _configuration["Servers:ML"] + PredictPath,
            posts);
        
        var contentStream = await response.Content.ReadAsStreamAsync();
        
        using var streamReader = new StreamReader(contentStream);
        using var jsonReader = new JsonTextReader(streamReader);
        
        var serializer = new JsonSerializer();
        
        try
        {
            var values = serializer.Deserialize<List<double>>(jsonReader);

            for (int i = 0; i < posts.Count; i++)
                posts[i].Virality = values[i] / 1000;

            return posts;
        }
        catch(JsonReaderException)
        {
            Console.WriteLine("Invalid JSON.");
            return new List<PostDto>();
        } 
    }
}