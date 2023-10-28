using webnangcao.Entities.Enumerables;

namespace webnangcao.Tools;

public class ERoleTool
{
    public static ERole ToERole(string roleString)
    {
        return roleString.ToLower() switch
        {
            "superadmin" => ERole.SUPERADMIN,
            "admin" => ERole.ADMIN,
            "user" => ERole.USER,
            _ => ERole.USER,
        };
    }

    public static string ToString(ERole erole)
    {
        return erole.ToString().ToUpper();
    }

    public static ERole GetHighestRole(IList<string> roles)
    {
        if (roles.Contains("SUPERADMIN"))
            return ERole.SUPERADMIN;
        else if (roles.Contains("ADMIN"))
            return ERole.ADMIN;
        else
            return ERole.USER;
    }
}