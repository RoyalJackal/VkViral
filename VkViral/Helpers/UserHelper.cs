using VkNet.Model;
using VkViral.Dto.Auth;

namespace VkViral.Helpers;

public static class UserHelper
{
    public static UserDto Map(User user) =>
        new UserDto
        {
            Name = user.FirstName + " " + user.LastName,
            Image = user.Photo200
        };
}