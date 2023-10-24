namespace webnangcao.Entities.Enumerables;

public class ERoleConverter
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
}