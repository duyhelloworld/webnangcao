using webnangcao.Entities;

namespace webnangcao.Tools;

public class UserTool
{
    public static string GetAuthorName(User user)
    {
        if (user == null)
            return "Không rõ";
        if (user.FirstName != null && user.LastName != null)
            return user.FirstName + " " + user.LastName;
        if (user.UserName != null)
            return user.UserName;
        return "Ẩn danh";
    }
}