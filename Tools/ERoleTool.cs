using webnangcao.Entities.Enumerables;

namespace webnangcao.Tools;

public class ERoleTool
{
    public static ERole ToERole(string roleString)
    {
        return roleString.ToLower().Trim() switch
        {
            "superadmin" => ERole.SuperAdmin,
            "admin" => ERole.Admin,
            "user" => ERole.User,
            _ => ERole.User,
        };
    }

    public static string ToString(ERole erole)
    {
        return erole.ToString();
    }

    public static ERole GetHighestRole(string[] roles)
    {
        if (roles.Contains("SuperAdmin"))
            return ERole.SuperAdmin;
        else if (roles.Contains("Admin"))
            return ERole.Admin;
        else
            return ERole.User;
    }
}