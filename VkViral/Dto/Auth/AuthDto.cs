namespace VkViral.Dto.Auth;

public class AuthDto
{
    public int TokenId { get; set; }

    public DateTimeOffset ExpirationTime { get; set; }
    
    public int UserId { get; set; }
}