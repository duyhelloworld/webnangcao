namespace webnangcao.Models.Securities;

public class AppSetting
{
    public string JwtSecretKey { get; set; } = null!;
    public string JwtIssuer { get; set; } = null!;
    public TimeSpan JwtValidTime { get; set; }
    public string FacebookAppId { get; set; } = null!;
    public string FacebookAppSecret { get; set; } = null!;
    public string GoogleClientId { get; set; } = null!;
    public string GoogleClientSecret { get; set; } = null!;
}