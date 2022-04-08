namespace VkViral.Dto.Posts;

public class PostDto
{
    public string GroupName { get; set; }
    
    public Uri GroupImg { get; set; }
    
    public DateTime? PublicationDate { get; set; }
    
    public string Text { get; set; }
    
    public List<Uri> Images { get; set; }
    
    public List<Uri> Videos { get; set; }
    
    public List<Uri> Audios { get; set; }
    
    public int Likes { get; set; }
    
    public int Comments { get; set; }
    
    public int Reposts { get; set; }
    
    public int Views { get; set; }
    
    public int GroupPostCount { get; set; }
    
    public int? GroupMemberCount { get; set; }
}