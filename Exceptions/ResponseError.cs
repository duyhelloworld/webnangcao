namespace webnangcao.Exceptions;

public class ResponseError
{
    public string Reason { get; set; } = "";
    public string? RecommmendSolution { get; set; } = "";
    public object? DataToFix { get; set; }
}