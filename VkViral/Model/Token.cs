namespace VkViral.Model;

public class Token
{
    public int Id { get; set; }
    
    public string Value { get; set; }
    
    public DateTimeOffset ExpirationTime { get; set; }
    
    public int UserId { get; set; }
}