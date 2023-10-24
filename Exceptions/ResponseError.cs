namespace webnangcao.Exceptions;

public class ResponseError
{
    public IEnumerable<string> Reasons { get; set; } = new List<string>();
    public IEnumerable<string>? RecommmendSolutions { get; set; } = new List<string>();
    public object? DataToFix { get; set; }
}