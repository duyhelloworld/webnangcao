using webnangcao.Entities;

namespace webnangcao.Tools;

public class UserTool
{
    public static string GetAuthorName(User user)
    {
        if (user.FirstName != null && user.LastName != null)
            return user.FirstName + " " + user.LastName;
        if (user.UserName != null)
            return user.UserName;
        return "Ẩn danh";
    }

    public static string GetAvatar(User user)
    {
        if (user.Avatar != null)
            return user.Avatar;
        return "default-avatar.jpg";
    }
}