namespace webnangcao.Tools;

public class TagTool
{
    public static string[] GetTags(string? tags)
    {
        if (string.IsNullOrWhiteSpace(tags))
            return Array.Empty<string>();
        return tags.Split(",", StringSplitOptions.RemoveEmptyEntries);
    }
}