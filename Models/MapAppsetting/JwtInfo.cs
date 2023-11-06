namespace webnangcao.Models.MapAppsetting;

public class JwtInfo
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public int ExpireDay { get; set; }
}